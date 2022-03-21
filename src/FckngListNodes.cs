using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTestProject;

public class FckngListNodes : IStartable
{
    const int MAX = 10000;

    private class ListNode<T>
    {
        public T value { get; set; }
        public ListNode<T> next { get; set; }
    }

    private static class IntLinkedListCreator
    {
        public static ListNode<int> CreateListNode(string text)
        {
            int indexOfStart = text.IndexOf('[');
            int indexOfEnd = text.IndexOf(']');
            if (indexOfStart == -1 || indexOfEnd == -1 || indexOfStart >= indexOfEnd)
                throw new ArgumentException($"Variable {nameof(text)} does not start with \"[\" or doesn not ends with \"]\"");

            string substring = text.Substring(indexOfStart + 1, indexOfEnd - 1) + ",";

            if (String.IsNullOrEmpty(substring) || substring == ",") return new ListNode<int>();

            ListNode<int> head = null;
            ListNode<int> current = null;

            var subPart = new StringBuilder(4);

            foreach (var c in substring)
            {
                if (c != ',')
                {
                    if (!Char.IsWhiteSpace(c)) subPart.Append(c);
                }
                else
                {
                    int parsedValue = Int32.Parse(subPart.ToString().Trim());
                    subPart.Clear();

                    if (head is null)
                    {
                        head = new ListNode<int>();
                        current = head;
                        current.value = parsedValue;
                    }
                    else
                    {
                        var newNode = new ListNode<int>
                        {
                            value = parsedValue
                        };

                        current.next = newNode;
                        current = current.next;
                    }
                }
            }

            if (head is null) head = new ListNode<int>();

            return head;
        }
    }

    private static void PrintLinkedList<T>(ListNode<T> head)
    {
        var sb = new StringBuilder("Linked List: ");
        var node = head;
        while (node is not null)
        {
            sb.Append(node.value).Append(", ");
            node = node.next;
        }
        sb.Remove(sb.Length - 2, 1);
        Console.WriteLine(sb.ToString());
    }

    public void Start()
    {
        ListNode<int> a = IntLinkedListCreator.CreateListNode("[1]");
        ListNode<int> b = IntLinkedListCreator.CreateListNode("[9999, 9999]");
        ListNode<int> l = IntLinkedListCreator.CreateListNode("[1, 2, 3, 4, 5, 6, 7]");

        PrintLinkedList(S_RearrangeLastN(l, 6));
    }

    private ListNode<int> S_Reverse_WithoutColections(ListNode<int> l)
    {
        ListNode<int> curr = l;
        ListNode<int> prev = null;

        while (curr is not null)
        {
            var next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
        }

        return prev;
    }

    private ListNode<int> S_Revers_WithStack(ListNode<int> l)
    {
        if (l is null) return null;
        if (l.next is null) return l;

        Stack<ListNode<int>> listStack = new Stack<ListNode<int>>();

        for (var n = l; n is not null; n = n.next)
        {
            listStack.Push(n);
        }

        ListNode<int> head = null;
        ListNode<int> prev = null;

        int count = listStack.Count;

        for (int i = 0; i < count; i++)
        {
            if (head is null)
            {
                head = listStack.Pop();
                prev = head;
            }
            else
            {
                var n = listStack.Pop();
                prev.next = n;
                prev = prev.next;
            }
        }
        prev.next = null;

        return head;
    }

    private ListNode<int> S_Reverse_WithList(ListNode<int> l)
    {
        if (l is null) return null;

        ListNode<int> newHead = null;

        List<ListNode<int>> listList = new List<ListNode<int>>();

        for (ListNode<int> n = l; n != null; n = n.next)
        {
            listList.Add(n);
        }

        int count = listList.Count;
        newHead = listList[count - 1];

        for (int i = count - 1; i >= 1; i--)
        {
            listList[i].next = listList[i - 1];
        }
        listList[0].next = null;

        return newHead;
    }

    private ListNode<int> S_RearrangeLastN(ListNode<int> l, int n)
    {
        if (n <= 0) return l;

        var head = l;

        int count = 0;
        for (var curr = l; curr != null; curr = curr.next)
        {
            count++;
        }

        if (count <= n) return l;

        int i = 0;
        for (var curr = l; curr != null;)
        {
            var tmpNext = curr.next;

            if (i == count - n - 1)
            {
                curr.next = null;
                head = tmpNext;
            }
            else if (i == count - 1)
            {
                curr.next = l;
            }

            curr = tmpNext;
            i++;
        }

        return head;
    }
    
    private ListNode<int> S_ReverseGroupsOfK(ListNode<int> l, int k)
    {
        if (k <= 1) return l;

        ListNode<int>[] buff = new ListNode<int>[k];
        ListNode<int> head = null;
        ListNode<int> prev = null;

        int count = 0;
        for (var n = l; n != null;)
        {
            buff[count] = n;
            n = n.next;

            if (count == k - 1)
            {
                if (head is null) head = buff[k - 1];
                if (prev is not null) prev.next = buff[k - 1];

                for (int i = k - 1; i > 0; i--)
                {
                    buff[i].next = buff[i - 1];
                }
                prev = buff[0];
                
                count = 0;
            }
            else count++;
        }

        if (count > 0)
        {
            prev.next = buff[0];
            buff[count - 1].next = null;
        }
        else
        {
            prev.next = null;
        }

        return head;
    }

    private ListNode<int> S_SumHugeNumber(ListNode<int> a, ListNode<int> b)
    {
        const int MAX = 10000;

        List<int> aList = new List<int>();
        List<int> bList = new List<int>();

        var aNode = a;
        var bNode = b;
        bool aIsNotNull = aNode != null;
        bool bIsNotNull = bNode != null;
        while (aIsNotNull || bIsNotNull)
        {
            if (aIsNotNull)
            {
                aList.Add(aNode.value);
                aNode = aNode.next;
                aIsNotNull = aNode != null;
            }
            if (bIsNotNull)
            {
                bList.Add(bNode.value);
                bNode = bNode.next;
                bIsNotNull = bNode != null;
            }
        }

        ListNode<int> result = null;

        int aLength = aList.Count;
        int bLength = bList.Count;
        int length = aLength >= bLength ? aLength : bLength;
        int overflow = 0;
        for (int i = 0; i < length; i++)
        {
            int aToAdd = 0;
            int bToAdd = 0;

            if (i < aLength)
            {
                aToAdd = aList[aLength - i - 1];
            }
            if (i < bLength)
            {
                bToAdd = bList[bLength - i - 1];
            }
            int sum = aToAdd + bToAdd + overflow;
            int toPut = sum % MAX;
            overflow = sum >= MAX ? 1 : 0;

            if (result is null)
            {
                result = new ListNode<int>();
                result.value = toPut;
            }
            else
            {
                var newNode = new ListNode<int>
                {
                    value = toPut
                };

                newNode.next = result;
                result = newNode;
            }
        }

        if (overflow > 0)
        {
            var newNode = new ListNode<int>
            {
                value = 1
            };

            newNode.next = result;
            result = newNode;
        }

        if (result is null) result = new ListNode<int>();

        return result;
    }
}
