﻿using DSA.DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for heap sort.
    /// </summary>
    public static class HeapSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSort<T>(this IList<T> list)
        {
            return HeapSort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSortDescending<T>(this IList<T> list)
        {
            return HeapSortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");

            return HeapSort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSortDescending<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");

            return HeapSortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            return HeapSort(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            return HeapSortDescending(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSort<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException("index");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            IList<T> minHeap;

            if (index == 0 && count == list.Count)
            {
                minHeap = MinHeapify(list, comparer);
            }
            else
            {
                var maxCount = index + count;
                var neededElements = new List<T>(count);
                for (int i = index; i < maxCount; i++)
                {
                    neededElements.Add(list[i]);
                }
                minHeap = MinHeapify(neededElements, comparer);
            }

            int curIndex = index;

            while (minHeap.Count > 0)
            {
                list[curIndex++] = minHeap[0];

                minHeap[0] = minHeap[minHeap.Count - 1];
                minHeap.RemoveAt(minHeap.Count - 1);
                NodeMinHeapifyDown(minHeap, 0, comparer);
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> HeapSortDescending<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException("index");
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            IList<T> maxHeap;

            if (index == 0 && count == list.Count)
            {
                maxHeap = MaxHeapify(list, comparer);
            }
            else
            {
                var maxCount = index + count;
                var neededElements = new List<T>(count);
                for (int i = index; i < maxCount; i++)
                {
                    neededElements.Add(list[i]);
                }
                maxHeap = MaxHeapify(neededElements, comparer);
            }

            int curIndex = index;

            while (maxHeap.Count > 0)
            {
                list[curIndex++] = maxHeap[0];

                maxHeap[0] = maxHeap[maxHeap.Count - 1];
                maxHeap.RemoveAt(maxHeap.Count - 1);
                NodeMaxHeapifyDown(maxHeap, 0, comparer);
            }

            return list;
        }

        /// <summary>
        /// Helper method to build a binary min heap for the ascending sorting. Returns a <see cref="IList{T}"/> representing the backing array of the heap.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements used for building the min heap.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns a <see cref="IList{T}"/> representing the backing array of the min heap.</returns>
        private static IList<T> MinHeapify<T>(IList<T> list, IComparer<T> comparer)
        {
            List<T> heap = new List<T>(list);

            if (heap.Count > 0)
            {
                // Building the binary heap
                int lastNodeWithChildrenIndex = (list.Count - 2) / 2;
                for (int i = lastNodeWithChildrenIndex; i >= 0; i--)
                {
                    NodeMinHeapifyDown(heap, i, comparer);
                }
            }

            return heap;
        }

        /// <summary>
        /// Performing heapify-down operation on the given node. Used for min heap.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> representing the backing array of the min heap.</param>
        /// <param name="nodeIndex">The zero-based index of the node for heapify-down operation.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void NodeMinHeapifyDown<T>(IList<T> list, int nodeIndex, IComparer<T> comparer)
        {
            var leftChildIndex = 2 * nodeIndex + 1;
            var rigthChildIndex = 2 * nodeIndex + 2;

            // while the current node has children
            while (leftChildIndex < list.Count)
            {
                // saving the index of the smallest node of the three(current node and its children)
                var smallestNodeIndex = nodeIndex;

                // compare right child with the smallest node(current node for now)
                if (rigthChildIndex < list.Count)// needed checking if there is a right node also
                    if (comparer.Compare(list[rigthChildIndex], list[smallestNodeIndex]) < 0)
                        smallestNodeIndex = rigthChildIndex;

                // compare left child with the smallest node(current node or right child)
                if (comparer.Compare(list[leftChildIndex], list[smallestNodeIndex]) < 0)
                    smallestNodeIndex = leftChildIndex;

                // if the current node is the smallest
                if (smallestNodeIndex == nodeIndex)
                    break;// no more adjustments are needed

                // else if one of the children is smaller that the current node we swap them
                var temp = list[nodeIndex];
                list[nodeIndex] = list[smallestNodeIndex];
                list[smallestNodeIndex] = temp;

                // continue downwards with the comparison
                nodeIndex = smallestNodeIndex;
                leftChildIndex = 2 * nodeIndex + 1;
                rigthChildIndex = 2 * nodeIndex + 2;
            }
        }

        /// <summary>
        /// Helper method to build a binary max heap for the ascending sorting. Returns a <see cref="IList{T}"/> representing the backing array of the heap.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements used for building the max heap.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns a <see cref="IList{T}"/> representing the backing array of the max heap.</returns>
        private static IList<T> MaxHeapify<T>(IList<T> list, IComparer<T> comparer)
        {
            List<T> heap = new List<T>(list);

            if (heap.Count > 0)
            {
                // Building the binary heap
                int lastNodeWithChildrenIndex = (list.Count - 2) / 2;
                for (int i = lastNodeWithChildrenIndex; i >= 0; i--)
                {
                    NodeMaxHeapifyDown(heap, i, comparer);
                }
            }

            return heap;
        }

        /// <summary>
        /// Performing heapify-down operation on the given node. Used for max heap.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> representing the backing array of the max heap.</param>
        /// <param name="nodeIndex">The zero-based index of the node for heapify-down operation.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void NodeMaxHeapifyDown<T>(IList<T> list, int nodeIndex, IComparer<T> comparer)
        {
            var leftChildIndex = 2 * nodeIndex + 1;
            var rigthChildIndex = 2 * nodeIndex + 2;

            // while the current node has children
            while (leftChildIndex < list.Count)
            {
                // saving the index of the biggest node of the three(current node and its children)
                var biggestNodeIndex = nodeIndex;

                // compare right child with the biggest node(current node for now)
                if (rigthChildIndex < list.Count)// needed checking if there is a right node also
                    if (comparer.Compare(list[rigthChildIndex], list[biggestNodeIndex]) > 0)
                        biggestNodeIndex = rigthChildIndex;

                // compare left child with the biggest node(current node or right child)
                if (comparer.Compare(list[leftChildIndex], list[biggestNodeIndex]) > 0)
                    biggestNodeIndex = leftChildIndex;

                // if the current node is the biggest
                if (biggestNodeIndex == nodeIndex)
                    break;// no more adjustments are needed

                // else if one of the children is bigger that the current node we swap them
                var temp = list[nodeIndex];
                list[nodeIndex] = list[biggestNodeIndex];
                list[biggestNodeIndex] = temp;

                // continue downwards with the comparison
                nodeIndex = biggestNodeIndex;
                leftChildIndex = 2 * nodeIndex + 1;
                rigthChildIndex = 2 * nodeIndex + 2;
            }
        }
    }
}
