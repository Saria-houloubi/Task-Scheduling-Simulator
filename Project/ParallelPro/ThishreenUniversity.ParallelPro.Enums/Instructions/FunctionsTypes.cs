using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThishreenUniversity.ParallelPro.Enums
{
    /// <summary>
    /// The funcitons that the user can use in the mips code
    /// </summary>
    public enum FunctionsTypes
    {

        
        /// <summary>
        /// Loads a value to a registery
        /// Example: LD F9 M(30)
        /// </summary>
        LD = 1,

        /// <summary>
        /// Stores a value in memory from registery or instant value
        /// Example: SD M(30) F9
        /// </summary>
        SD = 2,

        /// <summary>
        /// Adds two values and stores in registery or memory
        /// Example: ADD F9 F2 M(30)
        /// adds value in f2 and Memmory(30) and stores it in F9
        /// </summary>
        ADD = 3,

        /// <summary>
        /// SUBs two values and stores in registery or memory
        /// Example: SUB F9 F2 M(30)
        /// SUBs value in f2 and Memmory(30) and stores it in F9
        /// </summary>
        SUB = 4,

        /// <summary>
        /// Divides two values and stores in registery or memory
        /// Example: DIV F9 F2 M(30)
        /// Divides value in f2 and Memmory(30) and stores it in F9
        /// </summary>
        DIV = 5,

        /// <summary>
        /// Multiplys two values and stores in registery or memory
        /// Example: MULT F9 F2 M(30)
        /// Multiplys value in f2 and Memmory(30) and stores it in F9
        /// </summary>
        MULT = 6,

        /// <summary>
        /// Loops the program to the desierd flag
        /// </summary>
        LOOP = 7,
    }
}
