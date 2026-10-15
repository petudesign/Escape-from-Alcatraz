using UnityEngine;
namespace PrisonCoop
{
    [CreateAssetMenu(menuName="Prison Coop/Character Appearance")]
    public sealed class PrisonerAppearance : ScriptableObject
    {
        public int Face,HairStyle,Beard,ShirtStyle,AppearanceRevision;
        public bool IsGuard;
        public float FaceWidth=1,HeadHeight=1,JawWidth=1,NoseLength=1,Posture;
        public float Height=1.82f,Width=0.94f;
        public Color Skin=new Color(0.79f,0.59f,0.43f),Hair=new Color(0.13f,0.10f,0.07f);
        public Color Shirt=new Color(0.69f,0.66f,0.54f),Trousers=new Color(0.22f,0.22f,0.20f),Shoes=new Color(0.24f,0.17f,0.11f);
    }
}
