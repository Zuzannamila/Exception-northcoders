using System;
using System.Collections.Generic;

namespace Exceptions
{
    public class ActivityChecker
    {
        public bool CheckActiveState(Guid id)
        {
            var idString = id.ToString();
            var lastChar = idString.Substring(idString.Length - 1);
            //try
            //{
            //    return int.Parse(lastChar) % 2 == 0;
            //} catch (Exception exception)
            //{
            //    throw new ActivityException("Guids must not end on letters");
            //}
            if(int.TryParse(lastChar, out int num))
            {
                return num % 2 == 0;
            }
            else
            {
               throw new ActivityException("Guids must not end on letters");
            }
        }
        public int HowManyActive(List<string> ids)
        {
            int num = 0;
            foreach (string id in ids)
            {
                if(Guid.TryParse(id, out Guid guidId))
                {
                    var lastChar = id.Substring(id.Length - 1);

                    if (int.TryParse(lastChar, out int number))
                    {
                        if (number % 2 == 0)
                        {
                            num++;
                        }
                    }
                }
                else
                {
                    throw new ActivityException($"{id} is not a proper Guid.");
                }

            }
            return num;
        }  

    }

    public class ActivityException : Exception
    {
        public ActivityException (string message) : base(message) { }
    }
}



// class ActivityChecker
// a method which takes a Guid, returns a bool
// true - if last char is even digit
// false - if last char is odd digit
// if last char is letter - exception! 