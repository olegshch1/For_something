using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexing
{
    public class Searcher
    {
        private readonly string path;

        public Searcher(string path)
        {
            this.path = path;
        }

        public List<(string,int,int)> Search(string query)
        {
            var terms = query.Split(' ');
            var qstack = new Stack<string>();
            foreach(var term in terms)
            {
                switch (term)
                {
                    case "&":
                        {
                            var t1 = qstack.Pop();
                            var t2 = qstack.Pop();
                            qstack.Push(AndOp(t1, t2));
                            break;
                        }
                    case "|":
                        {
                            var t1 = qstack.Pop();
                            var t2 = qstack.Pop();
                            qstack.Push(OrOp(t1, t2));
                            break;
                        }
                    default:
                        {
                            qstack.Push(term);
                            break;
                        }
                }
            }

            if(qstack.Count == 1)
            {
                Find(qstack.Pop());
            }
        }
    }
}
