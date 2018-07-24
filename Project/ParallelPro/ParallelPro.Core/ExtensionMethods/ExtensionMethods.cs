using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// Holds the some shared methods to use for all
    /// </summary>
    public static class SharedMethods
    {
        /// <summary>
        /// Compares between two values
        /// </summary>
        /// <param name="correctValue">The correct solution</param>
        /// <param name="toCheckValue">The solution to check if right</param>
        /// <returns>The number of corect/wrong/null hits</returns>
        public static float CompareFiledsAndGetMark(int? correctValue, int? toCheckValue)
        {
            //The student mark
            float mark = 0;
            //If the studnet sloution is not right
            if (correctValue != toCheckValue)
            {
                //Check if the right solution is null
                if (correctValue is null)
                    //Get 0.25 out of the mark
                    mark -= 0.5f;
            }
            else
            {
                if (correctValue != null)
                    //If the answer is right add 1
                    mark += 1.0f;
            }
            return mark;
        }
        /// <summary>
        /// Overload version for strings
        /// </summary>
        /// <param name="correctValue"></param>
        /// <param name="toCheckValue"></param>
        /// <returns></returns>
        public static float CompareFiledsAndGetMark(string correctValue, string toCheckValue)
        {
            //The student mark
            float mark = 0;
            //To not care about cases
            correctValue = correctValue?.ToLower();
            toCheckValue = toCheckValue?.ToLower();
            //If the studnet sloution is not right
            if (correctValue != toCheckValue)
            {  //Check if the right solution is null
                if (correctValue == null)
                    //Get 0.25 out of the mark
                    mark -= 0.5f;
            }
            else
            {
                if (!string.IsNullOrEmpty(correctValue))
                    //If the answer is right add 1
                    mark += 1.0f;
            }

            return mark;
        }


    }

    /// <summary>
    /// Holds the extension methods for the <see cref="InstructionWithStatusModel"/>
    /// </summary>
    public static class InstructionExtensionMethods
    {
        /// <summary>
        /// Compares between the two instructions 
        /// +1  for evert correct value
        /// -1 for every unlike value
        /// -0.25 for every null value that has been set by the student
        /// </summary>
        /// <param name="computerSolution"></param>
        /// <param name="studentSolution"></param>
        /// <returns>The student mark</returns>
        public static float CompareInstructions(this InstructionWithStatusModel computerSolution, InstructionWithStatusModel studentSolution)
        {
            //The student mark
            float mark = 0;
            //Correct each filed
            mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.IssueCycle, studentSolution.IssueCycle);
            mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.ReadCycle, studentSolution.ReadCycle);
            mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.ExecuteCompletedCycle, studentSolution.ExecuteCompletedCycle);
            mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.WriteBackCycle, studentSolution.WriteBackCycle);

            return mark;
        }

    }

    /// <summary>
    /// Holds the extension methods for the <see cref="FunctionalUnitWithStatusModel"/>
    /// </summary>
    public static class FunctionalUnitsExtensionMethods
    {
        /// <summary>
        /// Compares between the two functional units 
        /// +1  for evert correct value
        /// -1 for every unlike value
        /// -0.25 for every null value that has been set by the student
        /// </summary>
        /// <param name="computerSolution"></param>
        /// <param name="studentSolution"></param>
        /// <returns>The student mark</returns>
        public static float CompareFunctionUnits(this FunctionalUnitWithStatusModel computerSolution, FunctionalUnitWithStatusModel studentSolution)
        {
            //The student mark
            float mark = 0;
            if (computerSolution.IsBusy)
            {   //Correct each filed
                mark += computerSolution.IsBusy == studentSolution.IsBusy ? 1 : 0;
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.Time, studentSolution.Time);
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.Operation, studentSolution.Operation);
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.WaitingOperationForSource01, studentSolution.WaitingOperationForSource01);
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.WaitingOperationForSource02, studentSolution.WaitingOperationForSource02);
                mark += computerSolution.IsSource01Ready == studentSolution.IsSource01Ready ? 1 : 0;
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.SourceRegistery01, studentSolution.SourceRegistery01);
                mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.TargetRegistery, studentSolution.TargetRegistery);
                var source02Mark = SharedMethods.CompareFiledsAndGetMark(computerSolution.SourceRegistery02, studentSolution.SourceRegistery02);
                if (source02Mark > 0)
                {
                    mark += computerSolution.IsSource02Ready == studentSolution.IsSource02Ready ? 1 : 0;
                    mark += source02Mark;
                }
            }
            return mark;
        }
    }

    /// <summary>
    /// Holds the extension methods for the <see cref="RegisterResultModel"/>
    /// </summary>
    public static class RegistersExtensionMethods
    {
        /// <summary>
        /// Compares between the two regiseters
        /// +1  for evert correct value
        /// -1 for every unlike value
        /// -0.25 for every null value that has been set by the student
        /// </summary>
        /// <param name="computerSolution"></param>
        /// <param name="studentSolution"></param>
        /// <returns>The student mark</returns>
        public static float CompareRegisters(this RegisterResultModel computerSolution, RegisterResultModel studentSolution)
        {
            //The student mark
            float mark = 0;
            //Correct each filed
            mark += SharedMethods.CompareFiledsAndGetMark(computerSolution.Operation, studentSolution.Operation);

            return mark;
        }
    }
}
