using UnityEngine;

public static class RandomId 
{
    public static int GetId(int current, int count)
    {
        var lastId = current;
        current = Random.Range(1, count);



        if (current == lastId)
        {
            if (current == 1)
            {
                current++;
            }
            else if (current == count - 1)
            {
                current--;
            }
        }
        return current;
    }
}
