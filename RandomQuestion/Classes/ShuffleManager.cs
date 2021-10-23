using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomQuestion.Classes
{
    public class ShuffleManager
    {
        static Random random = new Random();

        public static ArrayList Shuffle(ArrayList array)
        {
            for (int i = 0; i < 1000; i++)
            {
                var i1 = random.Next(array.Count);
                var i2 = random.Next(array.Count);

                var temp = array[i1];
                array[i1] = array[i2];
                array[i2] = temp;
            }
            return array;
        }

        public static ArrayList PickUpSubArray(ArrayList array, int nrQuestions)
        {
            var i1 = random.Next(array.Count-nrQuestions);
            return array.GetRange(i1, nrQuestions);
        }
    }
}
