using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LinqDataCalc {

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
        /// Calculates the factorial of any given value, from a provided range of values.
        /// </summary>
        /// <param name="values">List of input values</param>
        /// <returns>The list of factorials based on their original values</returns>
        /// <example>
        /// Calculates Factorial values for an IEnumerable.
        /// <code>var result = Enumerable.Range(1,10).Factorial();</code>
        /// </example> 
public static IEnumerable<int> Factorial(this IEnumerable<int> values) {
      foreach (var i in values)
        if (i == 0)
       yield return 1;
         else
       yield return Enumerable.Range(1, i).Aggregate((x, y) => x * y);
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
Func<int,long,long> XPowOfY = (x,y) => (long)Math.Pow(x,val);

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
Func<int,ulong> TenPowOfX = (x) => checked(Convert.ToUInt64(create10.Invoke()));
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
        /// <returns>A dictionary of type layout <<string,Tuple<int,int>> filled with random values</returns>
        /// <example>
        /// Get an IDictionary filled with string keys and corresponding integer random values.
        /// <code>var resRnd = new Dictionary<string,Tuple<int,int>>().DictionaryRndFill(10);</code>
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
        /// object[] types = { "###","hello1","!&",439,30,12,1.3,3.2m,new List<int>{1,3,5,78,14,24,40,9},'d',3283782378289,"2902",92.4m,"345,2",881.74m,30333.1434,-43902,(new int[]{10,490,20,103,40,30}),930 };
        /// var resTuple = types.AsNumberTuples();
        /// </code>
        /// </example>
public static IList<Tuple<int,ulong,long,double,decimal>> AsNumberTuples(this IEnumerable<object> elements) {

var Numerals = elements.Where(t => t is int || t is ulong || t is long || t is double || t is decimal);
List<Tuple<int,ulong,long,double,decimal>> numbertup = new List<Tuple<int,ulong,long,double,decimal>>();
foreach (var N in Numerals) {
	if(N is Int32) numbertup.Add(Tuple.Create((int)N,(ulong)0,(long)0,0.0,0.0m));
	if(N is UInt64) numbertup.Add(Tuple.Create(0,(ulong)N,(long)0,0.0,0.0m));
	if(N is Int64) numbertup.Add(Tuple.Create(0,(ulong)0,(long)N,0.0,0.0m));
	if(N is Double) numbertup.Add(Tuple.Create(0,(ulong)0,(long)0,(double)N,0.0m));
	if(N is Decimal) numbertup.Add(Tuple.Create(0,(ulong)0,(long)0,0.0,(decimal)N));
 }
return numbertup;
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
		
           if (verifyMask.Invoke(getElement(i)))
            s+= strElement[getElement(i)];
           else
			s+= "-";
        }
        finSet.Add(s.ToString());
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