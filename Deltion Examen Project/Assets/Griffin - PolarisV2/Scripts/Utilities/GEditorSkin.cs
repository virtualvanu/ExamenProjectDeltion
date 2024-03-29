using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pinwheel.Griffin.HelpTool
{
    //[CreateAssetMenu(fileName = "Skin", menuName = "Griffin/Skin")]
    public class GEditorSkin : ScriptableObject
    {
        private static GEditorSkin instance;
        public static GEditorSkin Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GEditorSkin>("Skin");
                    if (instance == null)
                    {
                        instance = ScriptableObject.CreateInstance<GEditorSkin>();
                    }
                }
                return instance;
            }
        }

        [SerializeField]
        private List<Texture2D> textures;
        public List<Texture2D> Textures
        {
            get
            {
                if (textures == null)
                {
                    textures = new List<Texture2D>();
                }
                return textures;
            }
            set
            {
                textures = value;
            }
        }

        public Texture2D GetTexture(string name)
        {
            return Textures.Find(t => t.name.Equals(name));
        }
    }
}
