using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Desgin
{
    /// <summary>
    /// The desgin time view model for the instruction item with status
    /// </summary>
    public class InstructionWithStatusItemDesgin : InstructionWithStatusModel
    {
        /// <summary>
        /// The inctance that will bind to for desgin data
        /// </summary>
        public static InstructionWithStatusItemDesgin  Instance => new InstructionWithStatusItemDesgin();

        public InstructionWithStatusItemDesgin()
        {
            this.Name = "ADD";
            this.TargetRegistery = "R8";
            this.SourceRegistery01= "R2";
            this.SourceRegistery02 = "R1";
            this.IssueCycle= 1;
            this.ReadCycle = 2;
            this.ExecuteCycle= 3;
            this.WriteBackCycle = 10;
        }
    }
}
