using System;
using Principles._1.SingleResponsibilityPrinciple;
using Principles._2.OpenClosedPrinciple;
using Principles._3.LiskovSubstitutionPrinciple;
using Principles._4.InterfaceSegregationPrinciple;
using Principles._5.DependencyInversionPrinciple;

namespace Principles
{
    public enum Modes
    {
        Exit,
        SingleResponsibilityPrinciple,
        OpenClosedPrinciple,
        LiskovSubstitutionPrinciple,
        InterfaceSegregationPrinciple,
        DependencyInversionPrinciple,
        Unknown = int.MaxValue
    }

    public static class ModesExtension
    {
        public static Modes GetMode(this string value)
        {
            Modes i;
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            return Enum.TryParse<Modes>(value, out i) ? i : Modes.Unknown;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine($"-------------------------------");
                foreach (Modes m in Enum.GetValues(typeof(Modes)))
                {
                    if (m == Modes.Unknown) continue;
                    Console.WriteLine($"{(int)m}-{m}");
                }
                Console.WriteLine($"-------------------------------");

                var mode = Console.ReadLine().GetMode();
                Console.Clear();

                switch (mode)
                {
                    case Modes.Exit:
                        return;
                    case Modes.SingleResponsibilityPrinciple:
                        var singleResponsibilityPrinciple = new SingleResponsibilityPrinciple();
                        singleResponsibilityPrinciple.PreDo();
                        singleResponsibilityPrinciple.Do(new SingleResponsibilityPrincipleDo1());
                        singleResponsibilityPrinciple.Do(new SingleResponsibilityPrincipleDo2());
                        break;
                    case Modes.OpenClosedPrinciple:
                        var openClosedPrinciple = new OpenClosedPrinciple(new OpenClosedPrincipleDo1());
                        openClosedPrinciple.AllDo();
                        openClosedPrinciple = new OpenClosedPrinciple(new OpenClosedPrincipleDo2());
                        openClosedPrinciple.AllDo();
                        break;
                    case Modes.LiskovSubstitutionPrinciple:
                        new LiskovSubstitutionPrincipleBase().Do();
                        new LiskovSubstitutionPrincipleType1().Do();
                        new LiskovSubstitutionPrincipleType11().Do();
                        new LiskovSubstitutionPrincipleType2().Do();
                        new LiskovSubstitutionPrincipleType22().Do();
                        break;
                    case Modes.InterfaceSegregationPrinciple:
                        new InterfaceSegregationPrinciple1().Do();
                        new InterfaceSegregationPrinciple2().Do();
                        var interfaceSegregationPrinciple3 = new InterfaceSegregationPrinciple3();
                        ((IInterfaceSegregationPrincipleDo1)interfaceSegregationPrinciple3).Do();
                        ((IInterfaceSegregationPrincipleDo2)interfaceSegregationPrinciple3).Do();
                        break;
                    case Modes.DependencyInversionPrinciple:
                        var dependencyInversionPrinciple = new DependencyInversionPrinciple();
                        dependencyInversionPrinciple.Do(new IDependencyInversionPrinciple1());
                        dependencyInversionPrinciple.Do(new IDependencyInversionPrinciple2());
                        break;
                }
            } while (true);
        }
    }
}
