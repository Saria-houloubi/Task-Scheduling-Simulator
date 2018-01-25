using System.Collections.Generic;
using ThishreenUniversity.ParallelPro.Enums;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// A class that defines the icons geometry points
    /// </summary>
    public static class Icons
    {

        #region Private Memebers
        /// <summary>
        /// An array which holds the geometric points for all the icons that we have 
        /// </summary>
        private static string[] mGeometryPoints = new string[] {
            //Lock login icon [0]
            @"M25,91.2a6.8,6.8,0,1,1-6.8-6.8A6.8,6.8,0,0,1,25,91.2Zm14.42-6.8a6.8,6.8,0,1,0,6.8,6.8A6.8,6.8,0,0,0,39.39,84.4Zm21.22,0a6.8,6.8,0,1,0,6.8,6.8A6.8,6.8,0,0,0,60.61,84.4Zm21.22,0a6.8,6.8,0,1,0,6.8,6.8A6.8,6.8,0,0,0,81.83,84.4ZM70.9,70.79H29a7.28,7.28,0,0,1-7.27-7.27l.14-26.42A7.29,7.29,0,0,1,27.43,30V24.91h0v-.43a22.28,22.28,0,0,1,6.82-16.1A22.51,22.51,0,0,1,50,2h.51c12,.34,21.83,10.63,21.83,22.93v5a7.28,7.28,0,0,1,5.93,7.15l-.14,26.42A7.28,7.28,0,0,1,70.9,70.79Zm-14.1-21A6.8,6.8,0,1,0,50,56.6,6.8,6.8,0,0,0,56.8,49.8Zm2.9-25A10,10,0,0,0,51,14.73a10.07,10.07,0,0,0-1.16-.07,9.66,9.66,0,0,0-6.49,2.5,9.81,9.81,0,0,0-3.27,7.3v5.36H59.7Z"
        };
  
        /// <summary>
        /// The list that will hold the geometry point 
        /// key as for the name of the icon 
        /// and the value is the geometry points
        /// </summary>
        private static List<KeyValuePair<ApplicationIcons, string>> mIcons = new List<KeyValuePair<ApplicationIcons, string>>
        {
            new KeyValuePair<ApplicationIcons, string>(ApplicationIcons.LoginUserIcon,mGeometryPoints[0]),
        };

        #endregion

        #region Functions 

        /// <summary>
        /// Function tha returns the geometry value of the icon that we want
        /// </summary>
        /// <param name="value">The icon that is passed that we want the points for it</param>
        /// <returns></returns>
        public static object GetGeometry(ApplicationIcons value)
        {
          return mIcons.Find(item=> item.Key == value).Value;
        }

        #endregion
    }
}
