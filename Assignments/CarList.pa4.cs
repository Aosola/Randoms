using System;
using System.Threading;

public partial class CarList
{
    // From CarList.Core.cs.
    private void InsertEntry
    ( 
        CarListEntry entry, 
        CarListEntry previous, 
        CarListEntry current 
    )
    {
        //Case: empty list
        if( previous == null && current == null )
        {
            this.head = entry;
            this.tail = entry;
        }
        //Case: insert at head
        else if( previous == null )
        {
            entry.Next = this.head;
            this.head = entry;
        }
        //Case: insert at tail
        else if( current == null )
        {
            this.tail.Next = entry;
            this.tail = entry;
        }
        //Case: insert in middle
        else
        {
            entry.Next = current;
            previous.Next = entry;
        }
        
        length ++;
    }

    // From CarList.Sort.cs
    public void SelectSort( Func< NhtsaCar, NhtsaCar, int > CarComparer )
    {
        //Checks for trivial case where number of elements <= 1
        if( this.length < 2)
        return;
       
       //Loops through all unsorted element and attach minimum element to
       //the end of list containing the sorted portion of list
        for( int i = this.length; i > 0; i--)
        {
            //Assumes the minimum element of unsorted portion to be first
            //element
            CarListEntry min = this.head;
            //previous1 holds pointer to entry before minimum
            CarListEntry previous1 = null;
            
            //Loops through element up till last unsorted while comparing min
            //to each element, if a smaller element is found, it is set to be 
            //the new min
            CarListEntry current = this.head;
            CarListEntry previous2 = null;
            for( int j = 1; j < i ; j++)
            {
                previous2 = current;
                current = current.Next;
                if( CarComparer( min.Car, current.Car ) > 0 )
                {
                    min = current;
                    previous1 = previous2;
                }
            }
            
            //Remove min
            //Case1: minimum is at tail so it remains at tail and becomes first
            //element of sorted list
            //Case2: minimum is at head
            if(min == this.head)
            {
                head = head.Next;
            }
            //Case3: minimum is anywhere else
            else if (min != this.tail)
            {
                previous1.Next = min.Next;
            }
            
            //Attach min to tail
            this.tail.Next = min;
            this.tail = min;
            min.Next = null;            
        }
    }
    
    // From CarList.Sort.cs
    public void InsertSort( Func< NhtsaCar, NhtsaCar, int > CarComparer )
    {
        //Checks for trivial case where number of elements <= 1
        if( this.length < 2)
        return;
        
        //Traverse through the list from first element till last element
        CarListEntry firstUnsorted = this.head.Next;
        CarListEntry previous1 = this.head;
        while( firstUnsorted != null )
        {
            //Traverse through the list and compare the first unsorted element
            //to every element from head till the one before first unsorted.
            //Case 1: When an element greater than first unsorted is found, 
            //first unsorted is inserted before the element
            //Case 2: If no element is greater than first unsorted, nothing is 
            //done.
            CarListEntry position = this.head;
            CarListEntry previous2 = null;
            while( position != firstUnsorted )
            {
                //If any element before the firstUnsorted is found to be greater
                //than firstUnsorted, firstUnsorted is inserted before the
                //the greater element
                if( CarComparer (firstUnsorted.Car, position.Car ) < 0 )
                {
                    //firstUnsorted is deleted from list by changing Next 
                    //pointer of previous to the element following
                    //firstUnsorted
                    previous1.Next = firstUnsorted.Next;
                    //length is reduced by 1 due to deletion
                    this.length--;
                    //firstUnsorted is inserted before the first element found
                    //to be greater
                    InsertEntry(firstUnsorted, previous2, position);
                    //The new firstUnsorted is the element after previous1
                    //(which was the element following firstUnsorted before
                    //firstUnsorted was moved)
                    firstUnsorted = previous1.Next;
                    break;
                }
                previous2 = position; 
                position = position.Next;
            }
            
            //Position == firstUnsorted when there are no elements greater in
            //the sorted list, thus, previous1 is moved to firstUnsorted and 
            //firstUnsorted becomes the element after current firstUnsorted
            if( position == firstUnsorted)
            {
                previous1 = firstUnsorted;
                firstUnsorted = firstUnsorted.Next;
            }
        }    
        
    }
}