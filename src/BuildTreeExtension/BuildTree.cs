using System;
using System.Linq;

namespace System.Collections.Generic
{
    public class Node<TNode, TKey> where TNode : class
    {
        public TKey Id { get; set; }
        public TNode Current { get; set; }
        public IList<Node<TNode, TKey>> Children { get; set; } = new List<Node<TNode, TKey>>();
    }

    public static class GroupEnumerable
    {
        public static IList<Node<TNode, TKey>> BuildTree<TNode, TKey>(
            this IEnumerable<TNode> source,
            Func<TNode, TKey> uniqueKey,
            Func<TNode, TKey> groupingKey) where TNode : class
        {
            var wrappedSource = source.Select(e => new Node<TNode, TKey>
            {
                Id = uniqueKey(e),
                Current = e
            });

            Func<TKey, bool> isDefaultValue = (TKey key) =>
            {
                return typeof(TKey).IsClass ? key == null : key.Equals(default(TKey));
            };
            var groups = wrappedSource.GroupBy(i => groupingKey(i.Current));
            var roots = groups.FirstOrDefault(g => isDefaultValue(g.Key)).ToList();

            if (roots.Count > 0)
            {
                var children = groups.Where(g => !isDefaultValue(g.Key)).ToDictionary(g => g.Key, g => g.ToList());

                for (int i = 0; i < roots.Count; i++)
                {
                    AddChildren(roots[i], children);
                }
            }
            return roots;
        }

        private static void AddChildren<TNode, TKey>(
            Node<TNode, TKey> root,
            IDictionary<TKey, List<Node<TNode,TKey>>> children) where TNode : class
        {
            if (children.ContainsKey(root.Id))
            {
                root.Children = children[root.Id];

                for (int i = 0; i < root.Children.Count; i++)
                {
                    AddChildren(root.Children[i], children);
                }
            }
            else
            {
                root.Children = new List<Node<TNode, TKey>>();
            }
        }
    }
}
