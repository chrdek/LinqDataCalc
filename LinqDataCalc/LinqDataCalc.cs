using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LinqDataCalc {
    /// <summary>
    /// Main LinqDataCalcExtensions Class implementation.
    /// </summary>
    public static class LinqDataCalcExtensions {
        /// <summary>   
        /// Retrieve the mode values in a sequence of numbers.
        /// (The most frequently occuring number - ordered)
        /// </summary>
        /// <param name="elements">The numeric sequence used as input</param>
        /// <returns>An IEnumerable KeyValuePair with Key=total per the mode value and Value=occuring value in the sequence.</returns>
        /// <example>
        /// Get the top used values in an IEnumerable.
        /// <code>int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240,-371,-940 };
        /// var num = num_seq.AsEnumerable();
        /// var result = num.ModeValues();
        /// </code>
        /// </example>
        public static IEnumerable<KeyValuePair<int,int>> ModeValues(this IEnumerable<int> elements) {
var FreqVal = elements.ToList().GroupBy(num => num).OrderByDescending(n=>n.Count()).Select(n=> new KeyValuePair<int,int>(n.Count(),n.Key));
return FreqVal.Where(v=>v.Key == FreqVal.First().Key);
}

        /// <summary>
        /// Return the median value in an -ordered- numeric sequence.
        /// </summary>
        /// <param name="elements">The numeric sequence used as input</param>
        /// <returns>Double median value for the selected sequence</returns>
        /// <example>
        /// Get the median value in an IEnumerable.
        /// <code>int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240 };
        /// var num = num_seq.AsEnumerable();
        /// double result = num.MedianValue();
        /// </code>
        /// </example>
public static double MedianValue(this IEnumerable<int> elements) {
return (elements.OddOrEven()) ? ( Convert.ToDouble(elements.ToList().Take(elements.Count()/2).Last()) +
                                  Convert.ToDouble(elements.ToList().Take((elements.Count()/2)+1).Last()) )/2
                              : Convert.ToDouble(elements.ToList().Take(elements.Count()/2+1).Last());
}

        /// <summary>
        /// Checks whether the total length of a sequence is odd or even.
        /// </summary>
        /// <typeparam name="T">Any kind of elements in a sequence</typeparam>
        /// <param name="elements">The data sequence to be checked</param>
        /// <returns>True or False depending on the result</returns>
        /// <example>
        /// Check whether length of IEnumerable is odd or even.
        /// <code> bool oddlen = new int[]{4,43,13,50,40}.OddOrEven();</code>
        /// </example>
public static bool OddOrEven<T>(this IEnumerable<T> elements) {
return  ((elements.Count() % 2) == 0);
}

        /// <summary>
        /// Compare the bytes in two 1-dimensional byte arrays.
        /// </summary>
        /// <param name="larray"></param>
        /// <param name="rarray"></param>
        /// <returns>True or False depending on the result</returns>
        /// <example>
        /// Check whether two 1-dimensional byte arrays are equal.
        /// <code>
        /// byte[] b1 = new byte[]{77,90,144,0,3,0,0,0,4,0,0,0,255,255,0,0,184,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,128,0,0,0,14,31,32,100};
        /// byte[] b2 = new byte[]{77,90,144,0,3,0,0,0,4,0,0,0,255,20,0,0,0,0,0,0,128,0,0,0,14,31,186,14,0,180,9,205,33,184,1,76,103,114};
        /// bool result = b1.CompareBytes(b2);
        /// </code>
        /// </example>
public static bool CompareBytes(this byte[]larray, byte[] rarray) {
return (!larray.Any() || !rarray.Any()) ? false : larray.Zip(rarray, (x,y) => x^y).Aggregate((x,inc) => x+inc) == 0;
}

        /// <summary>
        /// Checks two 1-dimensional byte arrays and returns a list of the positions where the bytes are different with the byte values.
        /// </summary>
        /// <param name="mainarr">The main byte array to be compared</param>
        /// <param name="diffarr">The secondary byte array to be compared against</param>
        /// <returns>An Dictionary with the different bytes and the array positions that contains the difference against.</returns>
        /// <example>
        /// Check two 1-dimensional byte arrays, and return a list of different bytes and the point of difference.
        /// <code>
        /// byte[] b1 = new byte[]{77,90,144,0,3,0,0,0,4,0,0,0,255,255,0,0,184,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,128,0,0,0,14,31,32,100};
        /// byte[] b2 = new byte[]{77,90,144,0,3,0,0,0,4,0,0,0,255,20,0,0,0,0,0,0,128,0,0,0,14,31,186,14,0,180,9,205,33,184,1,76,103,114};
        /// var result = b1.DiffBytes(b2);
        /// </code>
        /// </example>
public static IDictionary<int,int> DiffBytes(this byte[]mainarr,byte[]diffarr) {
int pos = 0;
IDictionary<int,int> diffs = new Dictionary<int,int>();
foreach (byte b in mainarr) {
var comp = (b ^ diffarr[pos]) == 0 ? 0 :b;
if (comp != 0) {
  diffs.Add(pos,comp);
  }
  pos++;
 }
 return diffs;
}

        /// <summary>
        /// Checks whether two 2-dimensional arrays of any element type are equal.
        /// </summary>
        /// <typeparam name="T">Any kind of elements in a sequence</typeparam>
        /// <param name="a">The first sequence to be compared</param>
        /// <param name="b">The second sequence to be compared against the first</param>
        /// <returns>True or False depending on the result</returns>
        /// <example>
        /// Check whether two 2-dimensinonal arrays of any type are equal.
        /// <code> int[,] intArray1 = new int[4,4]{{432,31,32,43},{324,321,55,31},{110,34,543,13},{90,321,453,12}};
        ///        int[,] intArray2 = new int[4,4]{{110,43,120,301},{54,312,321,91},{99,45,21,12},{9,32,45,152}};
        ///        bool res = intArray1.SequenceEquals(intArray2);
        /// 
        ///        byte[,] barray1 = new byte[3,3]{{132,56,32},{30,45,10},{92,100,48}};
        ///        byte[,] barray2 = new byte[3,3]{{132,56,32},{30,45,10},{92,100,48}};
        ///        bool res = barray1.SequenceEquals(barray2);
        /// </code>
        /// </example>
public static bool SequenceEquals<T>(this T[,] a, T[,] b) => a.Rank == b.Rank
    && Enumerable.Range(0, a.Rank).All(d=> a.GetLength(d) == b.GetLength(d))
    && a.Cast<T>().SequenceEqual(b.Cast<T>());


/// <summary>   
/// Hamming Weight Calculation for bit-depth of integers (UInt support of 32-bit integer values)
/// Utilizes direct "bit manipulation, bit swapping" algorithm for hamming weight calculation.
/// </summary>
/// <param name="in_x">The integer value to return the numerical weight for</param>
/// <returns>The calculated integer value of hamming weight</returns>
/// <example>
/// Calculate the resulting hamming weight of an integer value.
/// <code>
/// uint testVal = 0x10043091; //int values
/// int result = testVal.HammingWeight();
/// </code>
/// </example>
public static int HammingWeight(this uint in_x) {

//Initialized byte vector for shifting ops.
byte[] byteW = new byte[65536];
Func<ushort,int> BitAllocat = (in_x_val) => {
 int count = 0;
 while(in_x_val > 0) {
  count += in_x_val & 1;
  in_x_val >>=1;
  }
  return count;
};

 for(int k=0; k < 65536; k++) byteW[k] = (byte)BitAllocat((ushort)k);
  return byteW[in_x & 0xFFFF] + byteW[in_x >> 16];
} 

/// <summary>   
/// Hamming Weight Calculation for bit-depth of long integers (ULong support of 64-bit integer values)
/// Utilizes direct "bit manipulation, bit swapping" algorithm for hamming weight calculation.
/// </summary>
/// <param name="in_x">The long integer value to return the numerical weight for</param>
/// <returns>The calculated integer value of hamming weight</returns>
/// <example>
/// Calculate the resulting hamming weight of a long integer value.
/// <code>
/// ulong testVal = 0x8891930311; //large int values
/// int result = testVal.HammingWeight();
/// </code>
/// </example>
public static int HammingWeight(this ulong in_x) {

//Initialized byte vector for shifting ops.
byte[] byteW = new byte[65536];
Func<ushort,int> BitAllocat = (in_x_val) => {
 int count = 0;
 while(in_x_val > 0) {
  count += in_x_val & 1;
  in_x_val >>=1;
  }
  return count;
};

 for(int k=0; k < 65536; k++) byteW[k] = (byte)BitAllocat((ushort)k);
  return byteW[in_x & 0xFFFF] + 
         byteW[(in_x >> 16) & 0xFFFF] +
		 byteW[(in_x >> 32) & 0xFFFF] +
		 byteW[(in_x >> 48) & 0xFFFF];
}


        /// <summary>   
        /// Hamming distance calculation for comparing two strings of equal length
        /// Retrieves the different characters count in both strings
        /// </summary>
        /// <param name="leftStr">The left string part</param>
        /// <param name="rightStr">The right string part</param>
        /// <returns> Maximum integer value in case both strings are not equal in legth,
        /// Number of differences in both strings otherwise.
        /// </returns>
        /// <example>
        /// Retrieve a default value as result if strings are not equal or hamming comparison value.
        /// <code> int res = "ABCDHFGF".HammingDist("ABCDEFO9"); //res equals to 3
        /// int res = "ABC".HammingDist("AAABBBCCCDD77"); //res equals to MaxInt
        /// int res = "A8udhhG".HammingDist("A8udhhG"); //res equals to 0
        /// int res = "A8udhhG".HammingDist("A8UDHHG"); //res equals to 4
        /// </code>
        /// </example>
        public static int HammingDist(this string leftStr, string rightStr)
        {
            return (leftStr.Length != rightStr.Length) ? Int32.MaxValue
                                                        : leftStr.Zip(rightStr, (l, r) => l != r ? 1 : 0).Sum();
        }

        /// <summary>   
        /// Enum implementation for selecting multiple algorithms
        /// for hamming distance used for integer comparison.
        /// </summary>
        /// <example>
        /// Usage as below:
        /// <code>
        /// AlgoType.DistLoop // selects method 1
        /// AlgoType.DistXOR1 // selects method 2
        /// AlgoType.DistXOR2 // selects method 3
        /// </code>
        /// </example>
        public enum AlgoType
        {
            DistLoop,
            DistXOR1,
            DistXOR2
        };

        /// <summary>   
        /// Hamming Distance calculation between two integer numbers
        /// using different algorithm variants, multiple algo. selections.
        /// (Calculated bit-level differences between numerical values)
        /// </summary>
        /// <param name="leftNum">The leftmost number to compare with</param>
        /// <param name="rightNum">The rightmost number to be compared with</param>
        /// <param name="hamming_alg">The hamming distance algorithm selection
        /// Can only use: AlgoType.DistLoop, AlgoType.DistXOR1, AlgoType.DistXOR2
        /// for setting appropriate variant to use.
        /// </param>
        /// <returns></returns>
        /// <example>
        /// Calculating the hamming distance between two integers
        /// by using different methods.
        /// <code>int res = (-995).HammingDistAlgo((-48),LinqDataCalcExtensions.AlgoType.DistXOR1);
        /// int res = (-995).HammingDistAlgo((-48),LinqDataCalcExtensions.AlgoType.DistXOR2);
        /// int res = (-995).HammingDistAlgo((-48),LinqDataCalcExtensions.AlgoType.DistLoop);
        /// //Result in all cases above = 7
        /// </code>
        /// </example>
        public static int HammingDistAlgo(this int leftNum, int rightNum, AlgoType hamming_alg)
        {

            //recursive xor bitwise algo definitions.
            Func<int, int, int> compBitsXOR1 = null;
            Func<int, int, int> compBitsXOR2 = null;

            compBitsXOR1 = (left, right) => {

                if (left == 0 && right == 0)
                    return 0;

                int xord_res = left ^ right;
                int prev_bit = xord_res & 1;
                int xord_rem = xord_res >> 1;
                return prev_bit + compBitsXOR1(xord_rem, 0);
            };


            compBitsXOR2 = (left, right) => {

                if (left == 0 && right == 0)
                    return 0;

                int num = left ^ right;
                int cnt = 0;
                if ((num & 1) > 0) cnt++;
                int xor_rem = num >> 1;

                return cnt + compBitsXOR2(xor_rem, 0);
            };

            switch (hamming_alg)
            {   //initial loop-based algo.
                case AlgoType.DistLoop:
                    if (leftNum == 0 && rightNum == 0)
                        return 0;
                    int xord_res = leftNum ^ rightNum;
                    int bit_set = 0;
                    while (xord_res > 0)
                    {
                        bit_set += xord_res & 1;
                        xord_res >>= 1;
                    }
                    return bit_set;

                //xor bitwise shift recursive 1
                case AlgoType.DistXOR1: return compBitsXOR1(leftNum, rightNum);

                //xor bitwise shift recursive 2
                case AlgoType.DistXOR2: return compBitsXOR2(leftNum, rightNum);
                // in case you are not using hamming dist, retrieve -1 as result.
                default:
                    return -1;
            }
        }

        /// <summary>   
        /// Levenshtein distance for two input strings and edit differences for those.
        /// (Can be applied between two strings of equal or differing length in size)
        /// </summary>
        /// <param name="strLeft">The leftmost string to find edit diffs from</param>
        /// <param name="strRight">The rightmost string to compare edit diffs with</param>
        /// <returns>Calculated sum or edit distance per input set of strings as integer.</returns>
        /// <example>
        /// Calculate the levenshtein distance between strings left, right.
        /// <code>int result = "Paints".LevnDist("ants"); //result = 2
        /// int result = "Compute".LevnDist("Confuse"); // result  = 3
        /// </code>
        /// </example>
        public static int LevnDist(this string strLeft, string strRight)
        {
            int r = strLeft.Length;
            int c = strRight.Length;
            int[,] arr = new int[r + 1, c + 1];

            //Check for zero-based equality, return appropriate distance.
            if (r == 0) return c; if (c == 0) return r;
            int p = 0, q = 0;

            //Pre-allocate initial list of values for dist. calculation.
            Enumerable.Range(0, r + 1).ToList().ForEach(e => {
                arr[p, 0] = e;
                p++;
            });
            Enumerable.Range(0, c + 1).ToList().ForEach(e => {
                arr[0, q] = e;
                q++;
            });

            //Main levn/edit distance loop.
            Enumerable.Range(1, r).ToList().ForEach(row => {
                for (int k = 1; k <= c; k++)
                {
                    //validate for string equality and retrurn the appropriate
                    //edit distance as result.
                    int edit_dist = (strRight[k - 1] == strLeft[row - 1]) ? 0 : 1;

                    arr[row, k] = Math.Min(Math.Min(arr[row - 1, k] + 1, arr[row, k - 1] + 1),
                                                 arr[row - 1, k - 1] + edit_dist);
                } 
            });
            //retrieve the last value of row/col from the array of edits.
            return arr[r, c];
        }

        /// <summary>  
        /// A recursive Levenstein implementation utilizing memoized cache for faster execution time.
        /// (Can be applied between two strings of equal or differing length in size)
        /// </summary>
        /// <param name="leftStrIn">The leftmost string to find edit diffs from</param>
        /// <param name="rightStrIn">The rightmost string to compare edit diffs with</param>
        /// <param name="memo">The matrix to store the intermediary distance cost results in
        /// Note: Needs to be the same size as NxM such that N = length of leftStrIn, M = length of rightStrIn
        /// </param>
        /// <returns>Calculated sum or edit distance per input set of strings as integer.</returns>
        /// <example>
        ///  Calculate the levenshtein distance between strings left, right.
        /// <code>int result = "test2".LevnDistRecur("arst22",new int[("test2".Length)+1,("arst22".Length)+1]); // result = 3
        /// int result = "Sam".LevnDistRecur("Samantha",new int[("Sam".Length)+1,("Samantha".Length)+1]); // result = 5
        /// </code>
        /// </example>
        public static int LevnDistRecur(this string leftStrIn, string rightStrIn, int[,] memo)
        {

            if (leftStrIn.Length == 0)
                return rightStrIn.Length;
            if (rightStrIn.Length == 0)
                return leftStrIn.Length;

            if (memo[leftStrIn.Length, rightStrIn.Length] != 0)
            {
                return memo[leftStrIn.Length, rightStrIn.Length];
            }

            //accumulat. values for substitution cost.
            int acc = (leftStrIn[0] != rightStrIn[0]) ? 1 : 0;

            //corresponding values for edit distance deletion/insertion/substitution respectively.
            int deletion = 1 + LevnDistRecur(leftStrIn.Substring(1), rightStrIn, memo);
            int insertion = 1 + LevnDistRecur(leftStrIn, rightStrIn.Substring(1), memo);
            int substitution = acc + LevnDistRecur(leftStrIn.Substring(1), rightStrIn.Substring(1), memo);


            int res = Math.Min(
                      Math.Min(deletion, insertion),
                    substitution);

            //Use memoized cached result for outputting distance.
            memo[leftStrIn.Length, rightStrIn.Length] = res;

            return res;
        }

        /// <summary>
        /// Calculates the standard deviation value of integer numeric sequence.
        /// </summary>
        /// <param name="values">The numeric sequence used as input</param>
        /// <returns>Standard deviation double result</returns>
        /// <example>
        /// Calculates STDEVP().
        /// <code> double result = new int[]{10,23,13,50,4}.StandardDeviation();</code>
        /// </example>
        public static double StandardDeviation(this IEnumerable<int> values) {
var avg = values.Average();
return Math.Sqrt(values.Average(v=>Math.Pow(v-avg,2)));
}

        /// <summary>
        /// Calculates the standard deviation value of large integer numeric sequence.
        /// </summary>
        /// <param name="values">The numeric sequence used as input</param>
        /// <returns>Standard deviation double result</returns>
        /// <example>
        /// Calculates STDEVP() for doubles.
        /// <code> double result = new double[]{0.1124,2.311,4.11,2.94,5.51).StandardDeviation();</code>
        /// </example>
public static double StandardDeviation(this IEnumerable<long> values) {
var avg = values.Average();
return Math.Sqrt(values.Average(v=>Math.Pow(v-avg,2)));
}

        /// <summary>
        /// Calculates the standard deviation value of double numeric sequence.
        /// </summary>
        /// <param name="values">The numeric sequence used as input</param>
        /// <returns>Standard deviation double result</returns>
        /// <example>
        /// Calculates STDEVP() for long.
        /// <code> double result = new long[]{394392,93993,323993,49240,49329}.StandardDeviation();</code>
        /// </example>
public static double StandardDeviation(this IEnumerable<double> values) {
var avg = values.Average();
return Math.Sqrt(values.Average(v=>Math.Pow(v-avg,2)));
}

        /// <summary>
        /// Based on a number list of probability values and a secondary list of possible expected outcomes of that probability,
        /// retrieve the overall expected value of the frequency of occurence on the inital observation/event.
        /// </summary>
        /// <param name="probabilities">List of probability values</param>
        /// <param name="outcomes">List of expected outcome values</param>
        /// <returns>The expected outcome/frequency of occurence value</returns>
        /// <example>
        /// Calculates Expected value of two probability sequences.
        /// <code> double expected = new double[]{1,2,3,4,5,6,7,8}.ExpectedValue(Enumerable.Repeat(0.125,8));</code>
        /// </example> 
public static double ExpectedValue(this IEnumerable<double> probabilities, IEnumerable<double> outcomes) {
 return Math.Round( (probabilities.Zip(outcomes, (x,y) => x*y).Sum()),2 );
}

        /// <summary>
        /// Fibonacci sequence for a given number as input.
        /// </summary>
        /// <param name="numLimit">The max number that fibonacci seq. gets generated for</param>
        /// <returns>An IEnumerable with fibonacci numbers.</returns>
        /// <example>
        /// Get the resulting numeric fibonacci sequence.
        /// <code>
        /// var seq_out = 10.FiboSeq();
        /// var seq_out = 100.FiboSeq();
        /// </code>
        /// </example>
        public static IEnumerable<ulong> FiboSeq(this int numLimit)
        {
            List<ulong> FibonacciNumbers = new List<ulong>();
            Enumerable.Range(0, numLimit)
                      .ToList()
                      .ForEach(k => FibonacciNumbers.Add(k <= 1 ? 1 :
                                              FibonacciNumbers[k - 2] + FibonacciNumbers[k - 1]));
            return FibonacciNumbers;
        }

        /// <summary>
        /// For a provided maximum value, generate
        /// happy numbers sequence up to that value.
        /// </summary>
        /// <param name="endLimit">The numeric sequence upper limit to generate the happy numbers for</param>
        /// <returns>A list of integer numbers.</returns>
        /// <example>
        /// Get the integers in a sequence that produce a happy sum.
        /// <code>
        /// var list_out = 150.HappySeq();
        /// var list_out = 10.HappySeq();
        /// var list_out = 0.HappySeq(); //list_out Length = 0
        /// var list_out = 1.HappySeq(); //list_out Length = 1 (value = 1)
        /// </code>
        /// </example>
        public static IEnumerable<int> HappySeq(this int endLimit)
        {
            List<int> HappyNumbers = new List<int>();

            Func<int, int> happySum = null;
            happySum = (number) => {

                if (number == 0) return 0;
                // retrieve modulo, recursively call with number for squaring sums of digits or "happy sums".
                int n = number % 10;
                return n * n + happySum(number / 10);
            };

            //function used to verify that a provided number is happy num.
            Func<int, bool> isHappyNumber = (num) => {
                HashSet<int> retrieved = new HashSet<int>();
                while (num != 1 && !retrieved.Contains(num))
                {
                    retrieved.Add(num);
                    num = happySum(num);
                }
                return num == 1;
            };

            //Final step: Allocate a range of values and retrieve the happy numbers using
            // an output sequence.
            Enumerable.Range(1, endLimit).ToList()
                                        .ForEach(n =>
                                        {
                                            if (isHappyNumber(n))
                                                HappyNumbers.Add(n);
                                        });
            return HappyNumbers;
        }

        /// <summary>
        /// Fibonacci sequence yield utilizing generator functions.
        /// </summary>
        /// <param name="num">The max number that fibonacci seq. gets generated for</param>
        /// <returns>An IEnumerable with large fibonacci numbers.</returns>
        /// <example>
        /// Get the resulting numeric fibonacci sequence.
        /// <code>
        /// var seq_out = 10.FiboSeqGenerator();
        /// var seq_out = 100.FiboSeqGenerator();
        /// </code>
        /// </example>
        public static IEnumerable<ulong> FiboSeqGenerator(this int num)
        {
            int a_val = 1;
            int b_val = 0;
            int last;
            for (int i = 0; i < num; i++)
            {
                yield return (ulong)a_val;
                last = a_val;
                a_val += b_val;
                b_val = last;
            }
        }

        /// <summary>
        /// Calculates the factorial of any given value, from a provided range of values.
        /// </summary>
        /// <param name="values">List of input values</param>
        /// <returns>The list of factorials based on their original values</returns>
        /// <example>
        /// Calculates Factorial values for an IEnumerable.
        /// <code>var result = Enumerable.Range(1,10).Factorial();</code>
        /// </example> 
        public static IEnumerable<int> Factorial(this IEnumerable<int> values) {
      foreach (var i in values) {
        if (i == 0) {
       yield return 1;
}
         else {
       yield return Enumerable.Range(1, i).Aggregate((x, y) => x * y);
  }
 }
}


        /// <summary>
        /// Binary addition of two integer numbers incl. carry digits.
        /// (negative or positive integer numerical values only)
        /// </summary>
        /// <param name="from_x">Numerical value x to perform the addition to</param>
        /// <param name="add_y">Numerical value y to use in addition operation</param>
        /// <returns>The integer result of the addition operation</returns>
        /// <example>
        /// Retrieve the result of adding integer y to underlying value x or vice-versa.
        /// <code>
        /// int result1 = 100.add(93);
        /// int result2 = -90.add(54);
        /// </code>
        /// </example>
        public static int add(this int from_x, int add_y)
        {
            int result = 0,
                // carry digits from a and b 
                carry = from_x & add_y;

            if (Convert.ToBoolean(carry))
            {
                // Sum of bits of "a" and "b" where at least one 
                // of the bits is not set
                result = from_x ^ add_y;

                // carry is shifted by one so that adding it 
                // to "a" gives the required sum
                carry = carry << 1;

                result = add(carry, result);
            }
            else
            {
                result = from_x ^ add_y;
            }

            return result;
        }

        /// <summary>
        /// Binary subtraction of two integer numbers incl. carry digits.
        /// (negative or positive integer numerical values only)
        /// </summary>
        /// <param name="from_x">Numerical value x to perform the subtraction from</param>
        /// <param name="sub_y">Numerical value y to use in subtraction operation</param>
        /// <returns>The integer result of the subtraction operation</returns>
        /// <example>
        /// Retrieve the result of subtracting integer y from underlying value x or vice-versa.
        /// <code>
        /// int result = -483.subtract(53);
        /// int result = -483.subtract(403);
        /// </code>
        /// </example>
        public static int subtract(this int from_x, int sub_y)
        {

            // Iterate till there
            // is no carry
            while (sub_y != 0)
            {

                // set common set bits of y and negated on x
                int borrow = (~from_x) & sub_y;

                // Subtraction of bits of x
                // and y where at least one
                // of the bits is not set
                from_x = from_x ^ sub_y;

                // Borrow is shifted by one
                // so that subtracting it from
                // x gives the required sum
                sub_y = borrow << 1;
            }

            return from_x;
        }


        /// <summary>
        /// Raises a number to the Nth power recursively.
        /// </summary>
        /// <param name="value">The integer to raise to the Nth power</param>
        /// <param name="powBy">The Nth power value</param>
        /// <returns>Result of the power of a number</returns>
        /// <example>
        /// Calculates Nth power of an integer.
        /// <code>int result = new int[]{4,43,13,50,40}.Select(n=>n.PowerOf(3));</code>
        /// </example> 
        public static int PowerOf(this int value, int powBy) {
 return (powBy == 0) ? 1: value*PowerOf(value,(powBy - 1));
}

        /// <summary>
        /// Raises each number in a sequence to the Nth power.
        /// </summary>
        /// <param name="values">The list of integers to be raised to the Nth power</param>
        /// <param name="val">The Nth power value</param>
        /// <returns>Resulting list of large integers raised to the Nth power</returns>
        /// <example>
        /// Calculates Nth power of all integers in IEnumerable.
        /// <code>
        /// int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240,-371,-940 };
        /// var num = num_seq.AsEnumerable();
        /// var result = num.ToIntPowerOf(3);
        /// </code>
        /// </example> 
public static IEnumerable<long> ToIntPowerOf(this IEnumerable<int> values, int val) {
Func<int,long,long> XPowOfY = (x,y) => (long)Math.Pow(x,y);

foreach (var v in values) {
 yield return XPowOfY(v,val);
 }
}

        /// <summary>
        /// Raises each number in a sequence to the Nth power(where N is double).
        /// </summary>
        /// <param name="values">The list of integers to be raised to the Nth double power</param>
        /// <param name="val">The Nth power double value</param>
        /// <returns>Resulting list of double values raised to the Nth power</returns>
        /// <example>
        /// Calculates Nth double power of all integers in IEnumerable.
        /// <code> 
        /// int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240,-371,-940 };
        /// var num = num_seq.AsEnumerable();
        /// var result = num.ToDoublePowerOff(2.71);
        /// </code>
        /// </example> 
        public static IEnumerable<double> ToDoublePowerOf(this IEnumerable<int> values, double val) {
Func<int,double,double> XPowOfY = (x,y) => Math.Pow(x,val);

foreach (var dv in values) {
 yield return XPowOfY(dv,val);
 }
}

        /// <summary>
        /// Returns the value of 10 raised to the power of N value.
        /// </summary>
        /// <param name="powOf">The Nth power value</param>
        /// <returns>Result of the 10 in the power of N (can be float,int,ulong)</returns>
        /// <remarks>The function will not output negative values</remarks>
        /// <example>
        /// Calculates Nth power of number 10.
        /// <code>long tenthousand = (4).TenPowerOf(); </code>
        /// </example> 
public static object TenPowerOf(this int powOf) {
Func <string> create10 = () =>"10"+ new String('0',(powOf-1));
try {
Func<int,ulong> TenPowOfX = (x) => Convert.ToUInt64(create10.Invoke());
return TenPowOfX.Invoke(powOf);
 }
catch (OverflowException) { return (float)Math.Pow(10,powOf); }
}

        /// <summary>
        /// Retrieves a range of values from a list iteratively as a nested list.
        /// Element list at index 1 at indexes 1,2 at 1,2,3 up to N.
        /// </summary>
        /// <typeparam name="T">Any type of elements in the list</typeparam>
        /// <param name="elements">List of elements used as input</param>
        /// <param name="size">Number of max iterations that lists are retrieved</param>
        /// <returns>A nested enumerable list that has a maximum length defined by -size- parameter</returns>
        /// <example>
        /// Get the Nth iteration of an IEnumerable in nested form.        
        /// <code>
        /// int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240,-371,-940 };
        /// var num = num_seq.AsEnumerable();
        /// var result = num.IterateAt(4);
        /// </code>
        /// </example>
public static IEnumerable<IEnumerable<T>> IterateAt<T>(this IEnumerable<T> elements, int size) {
   var i = 1;
   while (i <= size)
   {
     yield return elements.Take(i);
     i++;
   }
}

        /// <summary>
        /// Retrieves chunks of size N from a list as a nested list.
        /// </summary>
        /// <typeparam name="T">Any type of elements in the list</typeparam>
        /// <param name="elements">List of elements used as input</param>
        /// <param name="len">Length of N - chunks that the elements list is to be splitted into</param>
        /// <returns>A nested enumerable list of which sub lists are chunks of equal length</returns>
        /// <example>
        /// Split an IEnumerable into chunks of Length N.
        /// <code>
        /// int[] num_seq = { -990,-940,-770,-599,-543,-513,-482,-451,-445,-371,-240,-371,-940 };
        /// var num = num_seq.AsEnumerable();
        /// var result = num.ChunkOf(3);
        /// </code>
        /// </example>
public static IEnumerable<IEnumerable<T>> ChunkOf<T>(this IEnumerable<T> elements, int len) {

   var pos = 0; 
   while (elements.Skip(pos).Any())
   {
      yield return elements.Skip(pos).Take(len);
      pos += len;
   }
}

        /// <summary>   
        /// Class implementation for BTree Nodes, BTree operations.
        /// </summary>
        /// <example>
        /// Usage as below:
        /// (This represents a set of nodes for a BTree of height = 3-including root Node-)
        /// <code>
        /// Node leaf1 = new Node(null, null);
        /// Node leaf2 = new Node(null, null);
        /// Node node = new Node(leaf1, null);
        /// Node root = new Node(node, leaf2);
        /// </code>
        /// </example>
        public class Node
        {
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }

            public Node(Node leftChild, Node rightChild)
            {
                this.LeftChild = leftChild;
                this.RightChild = rightChild;
            }
        }

        /// <summary>   
        /// Generate a binary tree of random height, based on a prespecified complexity
        /// factor value (needs to be of double data type).
        /// </summary>
        /// <param name="nodeDensity">The density factor of which to generate the random tree nodes with</param>
        /// <param name="depth">The b-tree depth for which to determine the height with</param>
        /// <returns>A randomly generated b-tree root with leaf nodes that consist of Node data type</returns>
        /// <example>
        /// Generate a binary tree of specified depth N with random nodes density.
        /// <code>
        /// var tree_root = 0.543.BTreeGen(6);
        /// </code>
        /// </example>
        public static Node BTreeGen(this double nodeDensity, int depth)
        {

            // Recursively construct the entire b-tree of depth= N
            // including randomiz. factor provided by nodeDensity.
            Random random = new Random();

            if (depth <= 0 || random.NextDouble() > nodeDensity)
            {
                return null;
            }

            return new Node(
                BTreeGen(nodeDensity, depth - 1),
                BTreeGen(nodeDensity, depth - 1));
        }

        /// <summary>   
        /// Generate a range of binary trees of random height, based on a prespecified complexity
        /// factor value (needs to be of double data type) and yields an enumerable based on those b-trees.
        /// </summary>
        /// <param name="depth">The b-tree depth for which to determine the height with</param>
        /// <param name="nodeDensity">The density factor of which to generate the random tree nodes with</param>
        /// <returns>An IEnumerable of multiple b-tree roots with random leaf nodes that consist of Node data type</returns>
        /// <example>
        /// Generate a binary tree range of specified depth N with random nodes density.
        /// <code>
        /// var ienum_tree_roots = LinqDataCalcExtensions.BTreeGen2(3,0.75); 
        /// </code>
        /// </example>
        public static IEnumerable<Node> BTreeGen2(int depth, double nodeDensity)
        {
            //Randomiz. factor for b-tree node density gen.
            Random random = new Random();

            if (depth <= 0 || random.NextDouble() > nodeDensity)
            {
                yield break;
            }

            yield return new Node(null, null);

            foreach (var leftChild in BTreeGen2(depth - 1, nodeDensity))
            {
                foreach (var rightChild in BTreeGen2(depth - 1, nodeDensity))
                {
                    yield return new Node(leftChild, rightChild);
                }
            }
        }

        /// <summary>   
        /// Calculation of a b-tree total height counting edges from 
        /// the root node, to the end of the leafs of the data structure.
        /// (Utilizes queuing on adding/removing nodes for height calculation)
        /// </summary>
        /// <param name="input">The root node of the b-tree of which to calculate the height from.</param>
        /// <returns>Calculated b-tree data structure height (root node = start).</returns>
        /// <example>
        /// Calculate the b-tree height from the root node to outer leafs.
        /// <code>
        /// var tree_root = 0.543.BTreeGen(6); //creates a root b-tree of size 6
        /// LinqDataCalcExtensions.BTreeHeight(tree_root); // result of total height 5
        /// </code>
        /// </example>
        public static int BTreeHeight(this Node input)
        {
            if (input == null)
                return 0;

            Queue<Node> queue = new Queue<Node>();
            int height = 0;
            queue.Enqueue(input);

            while (queue.Count > 0)
            {
                int nodesAtCurrentLevel = queue.Count;

                while (nodesAtCurrentLevel > 0)
                {
                    Node currentNode = queue.Dequeue();

                    if (currentNode.LeftChild != null)
                        queue.Enqueue(currentNode.LeftChild);

                    if (currentNode.RightChild != null)
                        queue.Enqueue(currentNode.RightChild);

                    nodesAtCurrentLevel--;
                }

                height++;
            }

            return height - 1; // Subtracting 1 as height is calculated as edges, not nodes.
        }



        /// <summary>
        /// Retrieve a list with random integer or decimal numbers depending on input provided.
        /// </summary>
        /// <param name="retIntSeq">The list used as input</param>
        /// <param name="number">A numeric value, decimal or integer</param>
        /// <returns>A list of random values</returns>
        /// <example>
        /// Get an IEnumerable filled with random integer/double values.
        /// <code>
        /// var resultInt = Enumerable.Repeat(0,10).IEnumRndFill(5); // fills with  random integers
        /// var resultDouble = Enumerable.Repeat(0,10).IEnumRndFill(1.5); // fills with random doubles
        /// </code>
        /// </example>        
        public static IEnumerable<object> IEnumRndFill(this IEnumerable<int> retIntSeq, object number) {

IList<object> retRndSeq = new List<object>();
using (RNGCryptoServiceProvider rngcrypto = new RNGCryptoServiceProvider())
{
for (int i=0; i < retIntSeq.Count(); i++) {
if (retIntSeq.Any()) {
 const int byte_lim = 4;
 byte[] rngSeq = new byte[byte_lim];
 rngcrypto.GetBytes(rngSeq);
 var outputInt = BitConverter.ToInt32(rngSeq,0);
 
 Func<int,int> intModify = (n) => Int32.Parse(n.ToString().Substring(0,byte_lim));
 Func<double> doubleModify = () => BitConverter.ToInt32(rngSeq,0)*.000001;
if (number is Int32) {
 retRndSeq.Add(intModify(outputInt));
 }
if (number is Double) {
 retRndSeq.Add(doubleModify());
 }
  }
 }
}
 return retRndSeq;
}

        /// <summary>
        /// Retrieve a dictionary with random integer numbers and a randomly produced string.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="length">The maximum amount of info produced</param>
        /// <returns>A dictionary of key=string/value=Tuple(int,int) type layout filled with random values</returns>
        /// <example>
        /// Get an IDictionary filled with string keys and corresponding integer random values.
        /// <code> // 1 - full code not shown.. Initialize a dictionary of key-value type key=string/value=Tuple(int,int)
        ///        //2 - call following method of the dictionary -> dict_var.DictionaryRndFill(10); </code>
        /// </example>
        public static IDictionary<string,Tuple<int,int>> DictionaryRndFill(this Dictionary<string,Tuple<int,int>> dictionary, int length) {

using (RNGCryptoServiceProvider rngcrypto = new RNGCryptoServiceProvider()) 
{
if (!dictionary.Any()) {
for (int i=1; i <= length; i++) {
 const int byte_lim = 4;
 byte[] rngSeq = new byte[byte_lim];
 rngcrypto.GetBytes(rngSeq);
 var outputInt = BitConverter.ToInt32(rngSeq,0);
 dictionary.Add(("RowSet"+i),Tuple.Create(Int32.Parse(outputInt.ToString().Substring(0,byte_lim)),
                                          Int32.Parse(outputInt.ToString().Substring(byte_lim,outputInt.ToString().Count()-(byte_lim+2)))) );
  }
 }
}
 return dictionary;
}

        /// <summary>
        /// Provided a list of integer values you, retrieve a list of tuples and random integer with random string values.
        /// </summary>
        /// <param name="elements">A list of default integers to be modified</param>
        /// <returns>A list of tuples with random integers and string values</returns>
        /// <remarks>This works using argument deconstruction by ValueTuple type (available with C# >= 7, .NET >= 4.7, VS 2019)
        /// Already included *.dll reference for ValueTuples (currently .NET = 4.6.*)
        /// </remarks>
        /// <example>
        /// Get an List Tuple filled with random integers and random strings.
        /// <code>var resRndInt = Enumerable.Range(1,10).RndIntTuple();</code>
        /// </example>
public static IList<(int sample, string name)> RndIntTuple(this IEnumerable<int> elements) {
  return elements.Select(i => (i * (new Random()).Next(1, 1000), $"{Path.GetRandomFileName()}")).ToList();
}

        /// <summary>
        /// Provided a list of integer values you, retrieve a list of tuples and random doubles with random string values.
        /// </summary>
        /// <param name="elements">A list of default integers to be modified</param>
        /// <param name="scaleFact">The scaling factor by which double values are created</param>
        /// <returns>A list of tuples with random doubles and string values</returns>
        /// <remarks>This works using argument deconstruction by ValueTuple type (available with C# >= 7, .NET >= 4.7, VS 2019)
        /// Already included *.dll reference for ValueTuples (currently .NET = 4.6.*)
        /// </remarks>
        /// <example>
        /// Get a List Tuple filled with random doubles and random strings.
        /// <code>var resRndDouble = Enumerable.Range(1,10).RndDoubleTuple(.001);</code>
        /// </example>
public static IList<(double sample, string name)> RndDoubleTuple(this IEnumerable<int> elements, double scaleFact) {
  return elements.Select(i => (i*(scaleFact*(new Random()).Next(1,1000)), $"{Path.GetRandomFileName()}")).ToList();
}

        /// <summary>
        /// Retrieve a list of numeric tuples from an input list of objects.
        /// </summary>
        /// <param name="elements">The list of objects to be used as input</param>
        /// <returns>A list of tuples with the relevant data types retrieved from the objects list</returns>
        /// <example>
        /// Get a List of zero-filled Tuples with numeric values (int/ulong/long/double/decimal).
        /// <code>
        /// object[] types = { "###","hello1","!*",439,30,12,1.3,3.2m,new List{1,3,5,78,14,24,40,9},'d',3283782378289,"2902",92.4m,"345,2",881.74m,30333.1434,-43902,(new int[]{10,490,20,103,40,30}),930 };
        /// var resTuple = types.AsNumberTuples();
        /// </code>
        /// </example>
public static IList<Tuple<int,ulong,long,double,decimal>> AsNumberTuples(this IEnumerable<object> elements) {

var Numerals = elements.Where(t => t is int || t is ulong || t is long || t is double || t is decimal);
List<Tuple<int,ulong,long,double,decimal>> numbertup = new List<Tuple<int,ulong,long,double,decimal>>();
foreach (var N in Numerals) {
	if(N is Int32) { numbertup.Add(Tuple.Create((int)N,(ulong)0,(long)0,0.0,0.0m)); }
	if(N is UInt64) { numbertup.Add(Tuple.Create(0,(ulong)N,(long)0,0.0,0.0m)); }
	if(N is Int64) { numbertup.Add(Tuple.Create(0,(ulong)0,(long)N,0.0,0.0m)); }
	if(N is Double) { numbertup.Add(Tuple.Create(0,(ulong)0,(long)0,(double)N,0.0m)); }
	if(N is Decimal) { numbertup.Add(Tuple.Create(0,(ulong)0,(long)0,0.0,(decimal)N)); }
 }
return numbertup;
}

        /// <summary>   
        /// Present a sequence of numbers in a string,
        /// into a 2-dimensional matrix (letters and some characters are ommited)
        /// that has zero-fill for extra positions in case that array is larger than input.
        /// </summary>
        /// <param name="StrNum">The string of numbers to be used as input</param>
        /// <param name="twodimOut">The 2d matrix to be used as output</param>
        /// <returns>A 2-dimensional integer matrix, of prespecified dimensions N x M</returns>
        /// <example>
        /// Retrieve ONLY numbers from a string represented in a 2d matrix array.
        /// Note: Minimum array dimensions are 2x2 (used as default when input length less than 4 or when overallocating array space).
        /// <code>
        /// int[,] result = "2911".ToIntMatrix(new int[4,4]); // Result OK with zero padding
        /// int[,] result = "2962728abcs1119__1".ToIntMatrix(new int[4,10]); //Result OK with zero padding
        /// int[,] result = "2962728abcs1119__1".ToIntMatrix(new int[4,2]); //Result OK, trimmed
        /// int[,] result = "2962728abcs1119__1".ToIntMatrix(new int[4,3]); //Result OK
        /// int[,] result = "2962728abcs1119__1".ToIntMatrix(new int[10,10]); //Result n/a, trimmed to 2x2 with zero padding
        /// int[,] result = "29".ToIntMatrix(new int[1,2]); //Result n/a, trimmed to 2x2 with zero padding
        /// </code>
        /// </example>
        public static int[,] ToIntMatrix(this string StrNum, int[,] twodimOut)
        {
            if (StrNum.Length < 4) return new int[2, 2];

            //twodimOut = new int[12,4];
            int k = 0;
            var vertic_idx = twodimOut.GetUpperBound(0) + 1; //Dimension limit 0 vertical 
            var horiz_idx = twodimOut.GetUpperBound(1) + 1; //Dimension limit 1 horizontal
            var onedim_tot = (vertic_idx * horiz_idx); //Full length of expanded 2 dimensional


            //Perform check only for exceeded boundaries of 1-dimensional array
            if (onedim_tot > StrNum.Length)
            {
                onedim_tot -= StrNum.Length;
                for (int n = 0; n < onedim_tot; n++)
                {
                    StrNum += "0";
                }
            }

            var onedim = Regex.Replace(StrNum, @"\D", "").Select(el => Int32.Parse((el + ""))).ToArray();

            //Check for uneven size of input array dimensions.
            var checkLen = ((twodimOut.GetLength(0) * twodimOut.GetLength(1)) > onedim.Length);
            //if the length is equal to the string input size, then you get a proper result.

            //Trim output 2d matrix dims in case is different than the original string length.
            if (checkLen == true)
            {
                vertic_idx = ((checkLen == true) && vertic_idx == horiz_idx) ? 2 : Math.Min(vertic_idx, horiz_idx);
                horiz_idx = ((checkLen == true) && vertic_idx == horiz_idx) ? 2 : Math.Min(vertic_idx, horiz_idx);
            }

            for (int i = 0; i < vertic_idx; i++)
            {
                for (int j = 0; j < horiz_idx; j++)
                {
                    twodimOut[i, j] = onedim[k];
                    k++;
                }
            }
            return twodimOut;
        }


        /// <summary>   
        /// Levenshtein edit distance matrix retrieval with full set of calculations.
        /// (Can be applied between two strings of equal or differing length in size)
        /// </summary>
        /// <param name="str1">The leftmost string to compare edit diffs from</param>
        /// <param name="str2">The rightmost string to compare edit diffs with</param>
        /// <returns>An enumerable of the entire 2-dimensional edit distances matrix with all distance calculations.</returns>
        /// <example>
        /// Calculate the levenshtein distance of two strings, return the entire set of iterations, 
        /// element at array position [N,M] is the result of the total diff distance.
        /// <code>var result_ienum = "test2".LevnDistEditMatrix("arst22"); //last matrix element = 3
        /// var result_ienum = "ant".LevnDistEditMatrix("aunt"); //last matrix element = 1
        /// </code>
        /// </example>
        public static IEnumerable<int[,]> LevnDistEditMatrix(this string str1, string str2)
        {
            int[,] editMatrix = new int[str1.Length + 1, str2.Length + 1];

            Enumerable.Range(0, str1.Length + 1).ToList().ForEach(e => {
                //for (int i = 0; i <= str1.Length; i++)
                //{
                //for (int j = 0; j <= str2.Length; j++)
                Enumerable.Range(0, str2.Length + 1).ToList().ForEach(m => {
                    //{
                    if (e == 0)
                    {
                        editMatrix[e, m] = m;
                    }
                    else if (m == 0)
                    {
                        editMatrix[e, m] = e;
                    }
                    else
                    {
                        int substitutionCost = (str1[e - 1] != str2[m - 1]) ? 1 : 0;
                        int deleteDistance = 1 + editMatrix[e - 1, m];
                        int insertDistance = 1 + editMatrix[e, m - 1];
                        int substituteDistance = substitutionCost + editMatrix[e - 1, m - 1];
                        editMatrix[e, m] = Math.Min(Math.Min(deleteDistance, insertDistance), substituteDistance);
                    }
                });
            });

            //Return the entire matrix with the edit distances iterations up to levenshtein total.
            //Final answer is in matrix's cell [N,M].
            yield return editMatrix;
        }

        /// <summary>
        /// Transposes a 2-dimensional array of different data type such that rows N become columns M and vice-versa.
        /// </summary>
        /// <typeparam name="T">Any kind of elements in a sequence</typeparam>
        /// <param name="matrixIn">The 2-dimensional input matrix to invert rows with columns from</param>
        /// <returns>Transposed 2-dimensional matrix of the original input array for any kind of array elements.</returns>
        /// <example>
        /// Transpose 2-dimensional matrix of dimensions [N,M].
        /// <code>
        /// int[,] matrixT1 = new int[6,2] { {3,4},{5,9},{10,44},{3,77},{88,0},{0,90} }; //test with integers matrix
        /// 
        /// double[,] matrixT2 = new double[5,9]{ {9.0,34.0,0.99,3.1,9.4,4.51,9.87,1.43,9.88}, {2.4,22.01,55.10,9.09,1.30,9.99,10.45,9.0,8.01},
        ///                                       {4.6,4.3,3.4,68.009,45.92,3.54,9.89,5.69,0.849},{1.2,4.5,33.5,54.4,45.9,5.89,9.09,1.223,4.54},
        ///                                       {4.5,1.0,4,42.9,9.9,9.9,9,9,9} }; //test with doubles matrix
        ///
        /// var resultT1 = matrixT1.MatrixTranspose(); // results in a 2x6 matrix
        /// var resultT2 = matrixT2.MatrixTranspose();  // results in a 9x5 matrix
        /// </code>
        /// </example>
        public static T[,] MatrixTranspose<T>(this T[,] matrixIn)
        {

            int row_len = matrixIn.GetLength(0);
            int col_len = matrixIn.GetLength(1);

            // Create a new transposed matrix with swapped dimensions
            T[,] transMatrix = new T[col_len, row_len];

            Enumerable.Range(0, row_len).AsParallel().ForAll(row => {
                for (int col = 0; col < col_len; col++)
                {
                    transMatrix[col, row] = matrixIn[row, col];
                }
            });

            return transMatrix;
        }

        /// <summary>
        /// Transposes a 2-dimensional jagged -or uneven- array of different data type such that rows N become columns M and vice-versa.
        /// </summary>
        /// <typeparam name="T">Any kind of elements in a sequence</typeparam>
        /// <param name="matrixIn">The 2-dimensional jagged array to invert rows with columns from</param>
        /// <returns>Transposed 2-dimensional jagged array of the original input array for any kind of array elements.</returns>
        /// <example>
        /// Transposed 2-dimensional jagged array of dimensions [N][M].
        /// <code>
        /// string[][] matrixT3 = new string[][]{ //test with strings array
        /// new string[12]{"This", "is a", "test","this","side","should","appear","on the left","of ","your","screen","if the"},
        /// new string[12]{" script"," has ","been run ","as", " expected.","Test ","run"," already ","for a ","matrix of ","nxm size "," where n is" },
        /// new string[12]{"equal to 3"," and ","m equal"," to "," twelve"," If n"," and  m","increase"," or decrease then","output should","vary ","accordingly"}
        /// };
        /// 
        /// var resultT3 = matrixT3.MatrixTranspose(); //results in a 12x3 jagged array [12][3]
        /// </code>
        /// </example>
        public static T[][] MatrixTranspose<T>(this T[][] matrixIn)
        {

            int row_len = matrixIn.Length;
            int col_len = matrixIn[0].Length;

            // Create a new transposed jagged array with swapped dimensions
            T[][] transMatrix = new T[col_len][];

            Enumerable.Range(0, col_len).AsParallel().ForAll(col => {
                transMatrix[col] = new T[row_len];
                for (int row = 0; row < row_len; row++)
                {
                    transMatrix[col][row] = matrixIn[row][col];
                }
            });

            return transMatrix;
        }


        /// <summary>
        /// Calculation of the dot product for integer vectors N -vector multiplication-
        /// </summary>
        /// <param name="v1">First integer array vector to multiply values from</param>
        /// <param name="v2">Second integer array vectr to multiply values with</param>
        /// <returns>An integer IEnumerable containing the dot product values based on the initial vectors</returns>
        /// <example>
        /// Calculate and retrieve the dot product of multiplying two vectors as an IEnumerable.
        /// <code>
        /// int[] vect1 = {2,3,3}; // First vector
        /// int[] vect2 = {3,2,2}; // Second vector
        /// var result = vect1.VectorProduct(vect2); // result is an IEnumerable list of multipl. values
        /// </code>
        /// </example>
        public static IEnumerable<int> VectorProduct(this int[] v1, int[] v2)
        {
            return v1.Zip(v2, (a, b) => a * b);
        }

        /// <summary>
        /// Matrix dot product calculation for 2-dimensional integer arrays of shape NxM.
        /// (Outputs the finalized product matrix in either list of lists or standard 2-d array format)
        /// </summary>
        /// <param name="mtrx1">First integer matrix to be used for multipl. product</param>
        /// <param name="mtrx2">Second integer matrix to be multiplied with for multipl. product</param>
        /// <param name="twoDim">Selection option to retrieve the product result as a list of sublists or as a normal 2-dimensional matrix</param>
        /// <returns>An Object of either List of sublists type or int[,] type that defines product matrix result.</returns>
        /// <example>
        /// Calculate and retrieve the dot product of multiplying two matrices in both list-of-lists format or 
        /// standard 2-dimensional array format. This can be determined by a variable, defaults to 2-dimensional array output.
        /// <code>
        /// var result_1 = new int[4,2]{{2,2},{3,7},{8,4},{8,5}}.MatrixProductv2(new int[2,3]{{1,2,4},{12,42,9}},0); // Results to 2-dim array output
        /// var result_2 = new int[4,2]{{2,2},{3,7},{8,4},{8,5}}.MatrixProductv2(new int[2,3]{{1,2,4},{12,42,9}},1); // Results to list of lists output
        /// </code>
        /// </example>
        public static object MatrixProductv2(this int[,] mtrx1, int[,] mtrx2, int twoDim = 0)
        {

            int row_dim = mtrx1.GetUpperBound(0);
            int col_dim = mtrx1.GetUpperBound(1);
            int row_dim2 = mtrx2.GetUpperBound(0);
            int col_dim2 = mtrx2.GetUpperBound(1);
            int[,] product_2dmatrix;

            //Change matrix2 based on dimensions of first.
            product_2dmatrix = (row_dim != row_dim2) ? new int[row_dim + 1, col_dim2 + 1]
                                                     : mtrx2;

            List<List<int>> twodim1 = new List<List<int>>();
            List<List<int>> twodim2 = new List<List<int>>();

            //Matrix 1 transf. to list of lists. (by rows indexing)
            twodim1 = mtrx1.Cast<int>().Select((x, i) =>
                 new { x, index = i / mtrx1.GetLength(1) })                 //Overload 'Select' and calc. row index.
                         .GroupBy(x => x.index)                           // Group on Row index
                        .Select(x => x.Select(n => n.x).ToList())         // Create List for each group.  
                       .AsParallel().ToList();

            //Matrix 2 transf. to list of lists. (by cols indexing)
            twodim2 = mtrx2.Cast<int>().Select((x, i) =>
                 new { x, index = i % mtrx2.GetLength(1) })                 //Overload 'Select' and calc. column index.
                         .GroupBy(x => x.index)                           // Group on Column index
                        .Select(x => x.Select(n => n.x).ToList())         // Create List for each group.  
                       .AsParallel().ToList();



            //Result as a 2-Dimensional Product Matrix = N x M
            for (int p = 0; p < row_dim + 1; p++)
            {
                for (int q = 0; q < col_dim2 + 1; q++)
                {
                    product_2dmatrix[p, q] = twodim1[p].Zip(twodim2[q], (x, y) => x * y).Sum();
                }
            }


            //Result as a list of sublists of length N = row_dim
            var nodim_matrix = Enumerable.Range(0, row_dim + 1)
                         .Select(row => Enumerable.Range(0, col_dim2 + 1)
                            .Select(col => product_2dmatrix[row, col])
                            .ToList())
                        .ToList();

            if (twoDim <= 0)
                return product_2dmatrix;
            else
                return nodim_matrix;
        }

        /// <summary>
        /// Matrix dot product calculation for 2-dimensional integer arrays of shape NxM.
        /// (PLINQ utilization for large data sets handling)
        /// </summary>
        /// <param name="matrixA">First integer matrix to be used for multipl. product</param>
        /// <param name="matrixB">Second integer matrix to be multiplied with for multipl. product</param>
        /// <returns>The product matrix in a 2-dimensional matrix format</returns>
        /// <example>
        /// Calculate and retrieve the dot product of multiplying the two input matrices.
        /// <code>
        /// var result = new int[4,2]{{2,2},{3,7},{8,4},{8,5}}.MatrixProduct(new int[2,3]{{1,2,4},{12,42,9}}); // Result as a 2-dim array
        /// </code>
        /// </example>
        public static int[,] MatrixProduct(this int[,] matrixA, int[,] matrixB)
        {
            int numRowsA = matrixA.GetLength(0);
            int numColsA = matrixA.GetLength(1);
            int numRowsB = matrixB.GetLength(0);
            int numColsB = matrixB.GetLength(1);

            numColsA = (numColsA != numRowsB) ? numRowsB : numColsA;
            int[,] result = new int[numRowsA, numColsB];

            // Use Parallel for large dataset handling.
            Enumerable.Range(0, numRowsA).AsParallel().ForAll(rowA =>
            {
                for (int colB = 0; colB < numColsB; colB++)
                {
                    int sum = 0;
                    for (int i = 0; i < numColsA; i++)
                    {
                        sum += matrixA[rowA, i] * matrixB[i, colB];
                    }
                    result[rowA, colB] = sum;
                }
            });

            return result;
        }

        /// <summary>
        /// Retrieve either the maximum or minimum value in a 2-dimensional integer matrix.
        /// </summary>
        /// <param name="mtrx">Input matrix to retrieve max-min values from</param>
        /// <param name="isMax">Option to retrieve either the maximum or minimum value from 2-dimensional dataset from</param>
        /// <returns>The maximum or minimum integer value of the input matrix</returns>
        /// <example>
        /// Retrieve the maximum, or minimum integer value from the input matrix.
        /// <code>
        /// int[,] arr2d = new int[,]{{10,40,13,40},{9,10,40,99},{9,10,40,99},{9,10,49,19},{9,10,40,990},{9,10,422,99}};
        /// var result = arr2d.MaxMatrixVal() // defaults to maximum value = 990
        /// var result = arr2d.MaxMatrixVal(false) // retrieve minimum value = 9
        /// </code>
        /// </example>
        public static int MaxMatrixVal(this int[,] mtrx, bool isMax = true)
        {
            if (mtrx.Length == 0) return 0;
            return (isMax) ? mtrx.Cast<int>().Max() : mtrx.Cast<int>().Min();
        }

        /// <summary>
        /// Retrieve either the maximum or minimum value in a jagged array data structure.
        /// </summary>
        /// <param name="mtrx">Input matrix to retrieve max-min values from</param>
        /// <param name="isMax">Option to retrieve either the maximum or minimum value from the jagged array dataset</param>
        /// <returns>The maximum or minimum integer value of the input array of arrays</returns>
        /// <example>
        /// Retrieve the maximum, or minimum integer value from the input array of arrays.
        /// <code>
        /// int[] arr1 = new int[] {1,2,3,5,4};
        /// int[] arr2 = new int[] {3,2,1,0,10,39,10,43,11};
        /// int[] arr3 = new int[]{24,1,90};
        /// int[][] main_arr = new int[][]{arr1,arr2,arr3};
        /// var result = main_arr.MaxMultiDimVal() // defaults to maximum value = 90
        /// var result = main_arr.MaxMultiDimVal(false) // retrieve minimum value = 0
        /// </code>
        /// </example>
        public static int MaxMultiDimVal(this int[][] mtrx, bool isMax = true)
        {
            if (!mtrx.Any()) return 0;
            //Return maximum or minumum values compared in multi-dimensional matrix.
            return (isMax) ? mtrx.SelectMany(el => el).Max() : mtrx.SelectMany(el => el).Min();
        }

        /// <summary>
        /// Retrieve either the maximum or minimum set of value from every nested integer list-of-lists data structure.
        /// </summary>
        /// <param name="inputColl">Input nested list to retrieve max-min set of values from</param>
        /// <param name="isMax">Option to retrieve either the maximum or minimum values per subset in every list-of-lists dataset</param>
        /// <returns>An integer list containing the maximum value of each column-compared subset in list-of lists</returns>
        /// <example>
        /// Retrieve all the maximum, or minimum integer values for each column-based subset in the input nested list.
        /// <code>
        /// // 1 - Provided that you initialize a list of lists (code not shown..)
        /// // . . . 
        /// allValues.Add(valuesSet1); // 2 - populate the lists of lists
        /// allValues.Add(valuesSet2);
        /// allValues.Add(valuesSet3);
        /// allValues.Add(valuesSet4);
        /// var result = allValues.MaxCollVals(); //defaults to maximum subset of values for every column-compared subset of lists
        /// var result = allValues.MaxCollVals(false); //results to minimum subset of values for every column-compared subset of lists
        /// </code>
        /// </example>
        public static List<int> MaxCollVals(this List<List<int>> inputColl, bool isMax = true)
        {

            if (!inputColl.Any()) return new List<int> { };
            //Showing the minimum or maximum values compared at each location per the input collection.
            return (isMax) ? inputColl.Aggregate((z1, z2) => z1.Zip(z2, (x, y) => Math.Max(x, y)).ToList())
                           : inputColl.Aggregate((z1, z2) => z1.Zip(z2, (x, y) => Math.Min(x, y)).ToList());
        }


        /// <summary>
        /// Retrieve all letter combinations (power sets) of a provided string.
        /// </summary>
        /// <param name="strElement">The string of letters used as input</param>
        /// <returns>A list of all combinations for the input string</returns>
        /// <example>
        /// Get a List of strings displaying letter combinations.
        /// <code>var combinations = ("He1l01!").LetterCombinationsOf();</code>
        /// </example>        
        public static List<string> LetterCombinationsOf(this string strElement) {

    int powerSetCount = Math.Abs((int)Math.Pow(2,strElement.Length));
    var finSet = new List<string>();
	
    for (int maskCnt = 0; maskCnt < powerSetCount; maskCnt++)
    {
	var s=""; foreach (var i in strElement)
        {
		Func<int,bool> verifyMask = (x) => ((maskCnt & (1 << x)) > 0);
		Func<char,int> getElement = (y) => strElement.IndexOf(y,0);
		
           if (verifyMask.Invoke(getElement(i))) {
            s+= strElement[getElement(i)];
           }
           else {
			s+= "-";
           }
        }
        finSet.Add(s);
    }
    return finSet;
}

        /// <summary>
        /// Retrieve a list of random elements from an input list.
        /// </summary>
        /// <typeparam name="T">Any type of elements in the list</typeparam>
        /// <param name="elements">The list of elements to be used as input</param>
        /// <param name="totElements">The maximum number of random elements to be retrieved</param>
        /// <returns>A list of random elements with specified total length</returns>
        /// <example>
        /// Get an IEnumerable of 10 randomly selected elements.
        /// <code>var tenElements = Enumerable.Range(1,100).GetRandomElements(10);</code>
        /// </example>
public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> elements, int totElements) {
var rnd = new Random();
return elements.OrderBy(x => ( rnd.Next() )).Take(totElements);
}

        /// <summary>
        /// Retrieve a list of random elements from an input list -using natural reordering-.
        /// </summary>
        /// <typeparam name="T">Any type of elements in the list</typeparam>
        /// <param name="elements"></param>
        /// <returns>A reording elements iterator result with the Random object seed</returns>
        /// <example>
        /// Get an IEnumerable of 10 reordered elements.
        /// <code>var tenElements = Enumerable.Range(1,10).ReorderElements().Take(10);</code>
        /// </example>
public static IEnumerable<T> ReorderElements<T>(this IEnumerable<T> elements) {
return elements.ReorderElementsIterator(new Random());
}

        /// <summary>
        /// Implementation of the iterative functionality for the list reording.
        /// </summary>
        /// <typeparam name="T">Any type of elements in the list</typeparam>
        /// <param name="elements">The list of elements to be used as input</param>
        /// <param name="prng">The Random object to be used as seed for shuffling indexes</param>
        /// <returns>A list of reordered elements using random index shuffling in the initial list</returns>
public static IEnumerable<T> ReorderElementsIterator<T>(this IEnumerable<T> elements, Random prng) {
List<T> listEl = elements.ToList(); var i = listEl.Count();
   while (i >= 1) {
    int j = prng.Next(i);
      yield return listEl[j];
		 
      listEl[j] = listEl[i-1];
		i--;
     }
}

 } //End Of LinqDataCalcExtensions
}