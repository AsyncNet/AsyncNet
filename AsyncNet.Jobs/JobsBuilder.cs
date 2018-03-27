using System;
using System.Collections.Generic;
using System.Linq;

namespace AsyncNet.Jobs
{
    public class JobsBuilder<T, TRes> where TRes : Job
    {
        private readonly Func<T, IEnumerable<T>> dependences;
        private readonly Func<T, T, bool> compare;
        private readonly Func<T, IEnumerable<TRes>, TRes> initializer;

        private class Item
        {
            public T Source { get; set; }

            public TRes Instance { get; set; }
        }

        public JobsBuilder(
            Func<T, IEnumerable<T>> dependences,
            Func<T, T, bool> compare,
            Func<T, IEnumerable<TRes>, TRes> initializer)
        {
            this.dependences = dependences;
            this.compare = compare;
            this.initializer = initializer;
        }

        public JobsBuilder(
            Func<T, IEnumerable<T>> dependences,
            Func<T, IEnumerable<TRes>, TRes> initializer)
            :this(dependences, (p1, p2) => p1.Equals(p2), initializer)
        {
        }

        public IEnumerable<TRes> GetJobs(IEnumerable<T> source)
        {
            var instances = new List<Item>();
            var stack = new Stack<T>();

            foreach (var obj in source)
            {
                GetTarget(obj, instances, stack);
            }

            return instances.Select(x => x.Instance);
        }

        private TRes GetTarget(T obj, List<Item> instances, Stack<T> stack)
        {
            if (stack.Count > 0 && stack.Contains(obj))
            {
                throw new ApplicationException("Circuit reference found");
            }

            var instanceContainer = instances.SingleOrDefault(x => compare(x.Source, obj));

            if (instanceContainer != null)
            {
                return instanceContainer.Instance;
            }

            var depencences = dependences(obj);
            var dependencesInstances = new List<TRes>();
            stack.Push(obj);

            foreach (var dep in depencences)
            {
                dependencesInstances.Add(GetTarget(dep, instances, stack));
            }

            stack.Pop();
            var instance = initializer(obj, dependencesInstances);
            instances.Add(new Item { Instance = instance, Source = obj });
            return instance;
        }
    }
}
