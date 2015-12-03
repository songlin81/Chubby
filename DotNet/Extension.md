    扩展方法的定义及使用

扩展方法是定义在静态类内部的静态方法，开发人员可以像用实例方法那样来使用扩展方法。
根据C#的语法规定，实例方法只能通过建立一个实例对象才能调用，不能通过类名来调用，相反的，静态方法只能通过类名来调用。
而扩展方法提供了一个新的机制可以在对象实例上调用静态方法。扩展方法主要用于在不改变现有的类型代码下，扩展现有类型的功能。
所扩展的功能只限于当前上下文有效，并不会对原有类型的定义的代码产生变化。

注意：扩展方法类和要扩展的类须具有相同的命名空间。
它的一般声明格式如下：
　　public static class 类名
　　{
　　　　public static 返回类型 扩展方法名 (this 要扩展的类型 参数名)
　　　　{
　　　　　　函数体;
　　　　}
　　}

class Program
    {
        static void Main(string[] args)
        {
            Complex com = new Complex(10, -10);
            var real = com.ToDouble();//调用扩展方法
            Console.WriteLine("复数"+com.ToValue()+"转换为double类型:"+real);
            Console.Read();
        }
    }

    public static class Extensions
    {
        /// <summary> 
        /// /// 把复数类型转换为double类型(Complex的扩展方法) 
        /// /// </summary> 
        /// /// <param name="com">复数</param> 
        /// /// <returns>双精度值</returns> 
        public static double ToDouble(this Complex com)
        {
            return com.Real;
        }

        //把复数转换为字符串类型(Complex的扩展方法)
        /// </summary>
        /// /// <param name="com">复数</param>
        /// /// <returns>字符串值</returns>
        public static string ToValue(this Complex com)
        {
            string str = com.Real.ToString();
            if (com.Imag > 0)
                str += "+";
            if (com.Imag != 0)
                str += com.Imag + "i";
            return str;
        }
    }

    public class Complex 
    { 
        //实数 
        protected double real; 
        public double Real { get { return real; } } 
        //虚数 
        private double imag;
        public double Imag { get { return imag; } }
        public Complex(double real, double imag)
        {
            this.real = real;
            this.imag = imag;
        }
    }