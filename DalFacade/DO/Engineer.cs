using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Engineer
(
    int Id,
    string Email,
    double Cost,
    string Name,
    DO.EngineerExperience Level
);
