//-----------------------------------------------------------------------
// <copyright file="SingleNodeList.cs" company="Graham D Wardle">
//     The Single node Linked list sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace SingleNode
{
    using System.Collections.Generic;

    /// <summary>
    /// The Class for a single Node Linked list.
    /// </summary>
    public class SingleNodeList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleNodeList" /> class. 
        /// </summary>
        /// <param name="value">The value to store in the list.</param>
        public SingleNodeList(int value)
        {
            this.Value = value;
            this.Next = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleNodeList" /> class.
        /// Used for moving the next node to a new
        /// </summary>
        /// <param name="value">The value to be stored.</param>
        /// <param name="next">The Next node to be moved </param>
        private SingleNodeList(int value, SingleNodeList next)
        {
            this.Value = value;
            this.Next = next;
        }

        /// <summary>
        /// Gets the name of the node.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Gets the next node List item.
        /// </summary>
        public SingleNodeList Next { get; private set; }

        /// <summary>
        /// The Method to add a new node.
        /// </summary>
        /// <param name="value">The value to be stored in the node.</param>
        public void AddNode(int value)
        {
            if (this.Value < value)
            {
                if (this.Next == null)
                {
                    this.Next = new SingleNodeList(value);
                }
                else
                {
                    this.Next.AddNode(value);
                }
            }
            else
            {
                SingleNodeList temp = new SingleNodeList(this.Value, this.Next);
                this.Value = value;
                this.Next = temp;
            }
        }

        /// <summary>
        /// The method to determine if the node list contains a value.
        /// </summary>
        /// <param name="value">The Value to test if it is contained in the linked list.</param>
        /// <returns>True if the Value matches; False if there no match.</returns>
        public bool Contains(int value)
        {
            if (this.Value == value)
            {
                return true;
            }
            else
            {
                if (this.Next != null)
                {
                    return this.Next.Contains(value);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// The method to get the list values in the node List.
        /// </summary>
        /// <returns>The List of node values.</returns>
        public List<int> GetList()
        {
            List<int> result = new List<int>();
            result.Add(this.Value);
            if (this.Next != null)
            {
                result.AddRange(this.Next.GetList());
            }

            return result;
        }
    }
}