//https://youtu.be/SF_JTs30xEY
//https://gist.github.com/nesteruk/3435834

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace ActiveMesa.R2P.Infrastructure
{
    internal class CSharpInProcessCompiler
    {
        internal Assembly Compile(string sourceCode, string compilerVersion = "v4.0")
        {
            CompilerParameters ps = PrepareCompilerParameters();

            var po = new Dictionary<string, string>
            {
                {"CompilerVersion", compilerVersion}
            };
            var p = new CSharpCodeProvider(po);
            var results = p.CompileAssemblyFromSource(ps, new[] {sourceCode});

            if (results.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (var e in results.Errors)
                    sb.AppendLine(e.ToString());
                throw new Exception(sb.ToString());
            }

            return results.CompiledAssembly;
        }

        internal Type CompileAndGetType(string sourceCode, string compilerVersion = "v4.0")
        {
            var ass = Compile(sourceCode, compilerVersion);
            var types = ass.GetTypes();
            if (types == null || types.Length == 0)
                throw new Exception("Compiled assembly produced no types");
            else
                return types[0];
        }

        internal object CompileAndInstantiate(string sourceCode, string compilerVersion = "v4.0")
        {
            var mainType = CompileAndGetType(sourceCode, compilerVersion);
            return Activator.CreateInstance(mainType);
        }

        private CompilerParameters PrepareCompilerParameters()
        {
            var ps = new CompilerParameters {GenerateInMemory = true, GenerateExecutable = false};

            // add everything in this AppDomain
            foreach (var reference in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    ps.ReferencedAssemblies.Add(reference.Location);
                }
                catch (Exception ex)
                {
                    string s = "Cannot add assembly " + reference.FullName + " as reference.";
                    Debug.WriteLine(s);
                }
            }

            return ps;
        }
    }
}