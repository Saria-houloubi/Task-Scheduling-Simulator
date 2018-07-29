using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// The Design time view model for the instruction item with status
    /// </summary>
    public class InstructionWithStatusItemDesign : InstructionWithStatusModel
    {
        /// <summary>
        /// The inctance that will bind to for Design data
        /// </summary>
        public static InstructionWithStatusItemDesign  Instance => new InstructionWithStatusItemDesign();

        public InstructionWithStatusItemDesign()
        {
            this.Name = ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.ADD;
            this.TargetRegistery = "R8";
            this.SourceRegistery01= "R2";
            this.SourceRegistery02 = "R1";
            this.IssueCycle= 1;
            this.ReadCycle = 2;
            this.ExecuteCompletedCycle= 3;
            this.WriteBackCycle = 10;
        }
    }
}
