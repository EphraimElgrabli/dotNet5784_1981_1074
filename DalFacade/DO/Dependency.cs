using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Dependency
(
  int Id,
  int DependentTask,
  int DependsOnTask
)
{
    public Dependency() : this(0) { } //empty ctor for stage 3

    
}
