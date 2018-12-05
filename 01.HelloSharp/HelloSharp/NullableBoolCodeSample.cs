using System;

namespace HelloSharp
{
    internal class NullableBoolCodeSample
    {
        public static void Run()
        {
            bool?[] vals = { true, false, null };
            string op = "&";
            foreach (var op1 in vals)
            {
                string sOp1 = op1.HasValue ? op1.ToString() : "null";
                foreach (var op2 in vals)
                {
                    var res = op1 & op2;

                    string sOp2 = op2.HasValue ? op2.ToString() : "null";
                    string sRes = res.HasValue ? res.ToString() : "null";
                    Console.WriteLine("{0,-5} {1} {2,-5} = {3,-5}", sOp1, op, sOp2, sRes);
                }
            }


            Console.WriteLine("---");
            op = "|";
            foreach (var op1 in vals)
            {
                string sOp1 = op1.HasValue ? op1.ToString() : "null";
                foreach (var op2 in vals)
                {
                    var res = op1 | op2;

                    string sOp2 = op2.HasValue ? op2.ToString() : "null";
                    string sRes = res.HasValue ? res.ToString() : "null";
                    Console.WriteLine("{0,-5} {1} {2,-5} = {3,-5}", sOp1, op, sOp2, sRes);
                }
            }
        }
    }
}
