using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pinwheel.Griffin.HelpTool
{
    [System.Serializable]
    public struct GHelpLink
    {
        [SerializeField]
        private string displayText;
        public string DisplayText
        {
            get
            {
                return displayText;
            }
            set
            {
                displayText = value;
            }
        }

        [SerializeField]
        private string link;
        public string Link
        {
            get
            {
                return link;
            }
            set
            {
                link = value;
            }
        }
    }
}
