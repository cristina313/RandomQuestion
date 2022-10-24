using System;
using System.Collections;

namespace RandomQuestion.Utils.Shuffling
{
    public class ShuffleManager
    {
        private static Random random = new Random();

        public static ArrayList Shuffle(ArrayList array)
        {
            for (int index = 0; index < 1000; index++)
            {
                var first = random.Next(array.Count);
                var second = random.Next(array.Count);

                var temp = array[first];
                array[first] = array[second];
                array[second] = temp;
            }
            return array;
        }

        public static ArrayList PickUpSubArray(ArrayList array, int nrQuestions)
        {
            var i1 = random.Next(array.Count - nrQuestions);
            return array.GetRange(i1, nrQuestions);
        }
    }
}
