namespace SimpleProject
{
    public class TestQl
    {
        class WithFoo
        {
            public void Foo(int i) { }
        }

        class WithoutFoo { }

        public void DoSomething()
        {
            //https://codeql.github.com/codeql-query-help/csharp/cs-invalid-dynamic-call/
            dynamic o = new WithoutFoo();
            o.Foo(3);
        }
    }
}
