using UnityEngine;

namespace PrisonCoop
{
    public sealed partial class PrisonEnvironment
    {
        // Design dimensions for this playable block; not a historical survey of Alcatraz.
        public const float TierHeight=3.35f;
        public const int CellTiers=3,CellsPerRow=10;
        private Material railPaint,cellSteel,redBracket;
        private void BuildCellBlock()
        {
            railPaint=Mat("Ivory gallery steel",new Color(0.75f,0.75f,0.65f));
            cellSteel=Mat("Aged ochre cell steel",new Color(0.42f,0.35f,0.23f));
            cellSteel.SetFloat("_SurfaceKind",3);
            redBracket=Mat("Oxide red bracket",new Color(0.36f,0.16f,0.12f));
            Box(transform,"Cellhouse foundation",new Vector3(-1.2f,-0.15f,4),new Vector3(38.4f,0.3f,16.6f),floor);
            Wall(transform,"South outer shell",new Vector3(-1.2f,0,-4.25f),new Vector3(38.4f,10.9f,0.24f));
            Wall(transform,"North outer shell",new Vector3(-1.2f,0,12.25f),new Vector3(38.4f,10.9f,0.24f));
            // East doorway leads to a covered control hall. West openings meet each stair landing.
            PortalWallX(18,-4.3f,12.3f,4,3.0f,10.9f,"Control hall entry");
            for(int tier=0;tier<CellTiers;tier++)
            {
                for(int i=0;i<CellsPerRow;i++)
                {
                    float x=-13.5f+i*3;
                    Cell(new Vector3(x,tier*TierHeight,0),0,101+i+tier*100,i==4&&tier==0);
                    Cell(new Vector3(x,tier*TierHeight,8),180,151+i+tier*100,false);
                }
                foreach(float z in new[]{0.15f,7.85f})
                    Line(transform,"Service pipe",new Vector3(-15,tier*TierHeight+2.92f,z),new Vector3(15,tier*TierHeight+2.92f,z),0.028f,iron,false);
                if(tier==0)continue;
                float y=tier*TierHeight;
                foreach(float z in new[]{0.80f,7.20f})
                {
                    Box(transform,"Gallery tier "+tier,new Vector3(0,y-0.11f,z),new Vector3(30.6f,0.22f,1.60f),floor);
                    Rail(new Vector3(-14.3f,y,z<4?1.65f:6.35f),new Vector3(15.3f,y,z<4?1.65f:6.35f));
                    for(int i=0;i<11;i++)Box(transform,"Gallery support corbel",new Vector3(-15+i*3,y-0.38f,z<4?0.65f:7.35f),new Vector3(0.18f,0.53f,1.2f),cream,false);
                }
                Box(transform,"West cross gallery",new Vector3(-14.8f,y-0.11f,4),new Vector3(1.0f,0.22f,4.8f),floor);
                Box(transform,"East cross gallery",new Vector3(16.65f,y-0.11f,4),new Vector3(2.7f,0.22f,8.2f),floor);
                Rail(new Vector3(-14.3f,y,1.65f),new Vector3(-14.3f,y,6.35f));
                Rail(new Vector3(15.3f,y,1.65f),new Vector3(15.3f,y,6.35f));
                foreach(float x in new[]{-17.8f,16.6f})LightAt(transform,new Vector3(x,y+2.3f,4),new Color(0.86f,0.91f,1),1.1f,6,false);
            }
            for(int i=0;i<5;i++)LightAt(transform,new Vector3(-12+i*6,4.9f,4),new Color(0.90f,0.92f,0.91f),1.15f,9,false);
            // Side roof strips leave an actual clerestory opening down the centre.
            foreach(float z in new[]{-1.2f,9.2f})Box(transform,"Roof side strip",new Vector3(-1.2f,11.0f,z),new Vector3(38.6f,0.25f,6.2f),cream);
            for(int i=0;i<7;i++)
            {
                float x=-18+i*5.7f;
                Box(transform,"Roof cross beam",new Vector3(x,10.7f,4),new Vector3(0.30f,0.60f,16.6f),cream);
                Box(transform,"Skylight glazing",new Vector3(x+2.7f,11.03f,4),new Vector3(5.1f,0.08f,4.0f),glass,false);
                for(int bar=0;bar<6;bar++)Box(transform,"Skylight security bar",new Vector3(x+0.35f+bar*0.95f,10.97f,4),new Vector3(0.04f,0.05f,4.05f),railPaint,false);
                LightAt(transform,new Vector3(x+2,9.9f,4),new Color(0.81f,0.89f,1),1.7f,11,false);
                if(i%2==1)Pendant(new Vector3(x+1.2f,8.6f,4));
            }
            for(int i=0;i<13;i++)
            {
                float x=-18.5f+i*2.8f;
                Window(new Vector3(x,10.20f,-4.10f),0);Window(new Vector3(x,10.20f,12.10f),180);
            }
            foreach(float z in new[]{0.15f,7.85f})
                Line(transform,"Service riser",new Vector3(15.8f,0.1f,z),new Vector3(15.8f,9.5f,z),0.04f,rust,false);
            Box(transform,"Block sign backing",new Vector3(-15.45f,2.8f,1.1f),new Vector3(0.08f,0.55f,1.8f),dark,false);
            Sign(transform,"B BLOCK",new Vector3(-15.39f,2.8f,1.1f),new Vector3(0,-90,0),0.14f);
            Sign(transform,"STAIRS  /  ALL TIERS",new Vector3(-15.25f,2.65f,0.85f),new Vector3(0,-90,0),0.13f);
            Sign(transform,"MOVEMENT CONTROL",new Vector3(17.84f,2.7f,4),new Vector3(0,90,0),0.13f);
        }

        private void PortalWallX(float x,float minZ,float maxZ,float openingZ,float width,float height,string name)
        {
            float a=openingZ-width/2,b=openingZ+width/2;
            Wall(transform,name+" side",new Vector3(x,0,(minZ+a)/2),new Vector3(0.24f,height,a-minZ));
            Wall(transform,name+" side",new Vector3(x,0,(b+maxZ)/2),new Vector3(0.24f,height,maxZ-b));
            Box(transform,name+" lintel",new Vector3(x,(height+2.8f)/2,openingZ),new Vector3(0.24f,height-2.8f,width),cream);
        }
        private void PortalWallZ(Transform root,float z,float minX,float maxX,float openingX,float width,string name)
        {
            float a=openingX-width/2,b=openingX+width/2;
            Wall(root,name+" side",new Vector3((minX+a)/2,0,z),new Vector3(a-minX,3.6f,0.24f));
            Wall(root,name+" side",new Vector3((b+maxX)/2,0,z),new Vector3(maxX-b,3.6f,0.24f));
            Box(root,name+" lintel",new Vector3(openingX,3.2f,z),new Vector3(width,0.8f,0.24f),cream);
        }

        private void Stairs()
        {
            var root=new GameObject("Open supervised stair bay").transform;root.SetParent(transform,false);
            Wall(root,"Stair outer wall",new Vector3(-20.4f,0,4.1f),new Vector3(0.22f,10.5f,8.6f));
            foreach(float z in new[]{-0.2f,8.4f})Wall(root,"Stair end wall",new Vector3(-17.9f,0,z),new Vector3(5.0f,10.5f,0.22f));

            for(int tier=0;tier<CellTiers;tier++)
            {
                float y=tier*TierHeight;
                // Only the arrival strip has a slab. Both flight footprints remain open above.
                if(tier>0)Box(root,"Full tier landing "+tier,new Vector3(-17.9f,y-0.11f,0.75f),new Vector3(5.2f,0.22f,1.9f),floor);
                Sign(root,"TIER "+(tier+1),new Vector3(-20.25f,y+1.8f,0.8f),new Vector3(0,-90,0),0.15f);
                LightAt(root,new Vector3(-17.8f,y+2.7f,3.4f),new Color(0.91f,0.93f,1),1.6f,6,false);
                if(tier==CellTiers-1)continue;
                Box(root,"Half landing "+tier,new Vector3(-17.9f,y+TierHeight/2-0.11f,6.4f),new Vector3(5.0f,0.22f,1.8f),floor);
                Flight(root,-16.65f,y,1.75f,1);
                Flight(root,-19.05f,y+TierHeight/2,5.55f,-1);
                Rail(new Vector3(-20.25f,y+TierHeight/2,7.25f),new Vector3(-15.55f,y+TierHeight/2,7.25f));
            }
        }
        private void Flight(Transform root,float x,float baseY,float startZ,int direction)
        {
            const int steps=10;const float tread=0.38f;float rise=TierHeight/20;
            for(int i=0;i<steps;i++)
            {
                float h=(i+1)*rise;
                Box(root,"Stair tread",new Vector3(x,baseY+h-0.09f,startZ+direction*(i+0.5f)*tread),new Vector3(1.95f,0.18f,tread+0.01f),floor);
                Box(root,"Worn stair nose",new Vector3(x,baseY+h+0.004f,startZ+direction*(i+0.06f)*tread),new Vector3(1.55f,0.008f,0.032f),scuffedIron,false);
            }
            foreach(float side in new[]{-1f,1f})
            {
                float railX=x+side*1.01f;
                Line(root,"Stair handrail",new Vector3(railX,baseY+1.0f,startZ),new Vector3(railX,baseY+TierHeight/2+1.0f,startZ+direction*steps*tread),0.045f,railPaint,false);
                float slope=Mathf.Atan2(TierHeight/2,steps*tread)*Mathf.Rad2Deg;
                var safety=Box(root,"Stair safety panel",new Vector3(railX,baseY+TierHeight/4+0.52f,startZ+direction*steps*tread/2),new Vector3(0.05f,1.0f,Mathf.Sqrt(steps*tread*steps*tread+TierHeight*TierHeight/4)),railPaint,true,false);
                safety.transform.localRotation=Quaternion.Euler(-direction*slope,0,0);safety.GetComponent<Renderer>().enabled=false;
                for(int i=0;i<=5;i++)Line(root,"Stair baluster",new Vector3(railX,baseY+i*rise*2,startZ+direction*i*tread*2),new Vector3(railX,baseY+i*rise*2+1,startZ+direction*i*tread*2),0.025f,railPaint,false);
            }
        }

        private void TransferHall()
        {
            var root=new GameObject("Movement control and yard approach").transform;root.SetParent(transform,false);
            Box(root,"Control hall floor",new Vector3(23,-0.15f,4.5f),new Vector3(10,0.3f,7),floor);
            PortalWallZ(root,1,18,28,24,2.8f,"Dining entrance");
            PortalWallZ(root,8,18,28,24,2.8f,"Yard corridor entry");
            Wall(root,"Control hall east wall",new Vector3(28,0,4.5f),new Vector3(0.24f,3.6f,7));
            Box(root,"Control hall roof",new Vector3(23,3.7f,4.5f),new Vector3(10.2f,0.2f,7.2f),cream);
            // A booth beside the route; it does not obstruct the playable circulation.
            Wall(root,"Booth back",new Vector3(20.2f,0,7.6f),new Vector3(3.0f,2.8f,0.13f));
            Box(root,"Booth desk",new Vector3(20.2f,0.95f,5.5f),new Vector3(3,0.16f,0.8f),wood);
            for(int i=0;i<12;i++)Box(root,"Control window bars",new Vector3(18.8f+i*0.25f,1.95f,5.85f),new Vector3(0.028f,1.9f,0.028f),iron,false);
            Sign(root,"MOVEMENT CONTROL",new Vector3(20.2f,2.65f,5.40f),Vector3.zero,0.11f);
            Sign(root,"DINING HALL",new Vector3(24,2.6f,1.14f),new Vector3(0,180,0),0.14f);
            Sign(root,"YARD  /  ESCORT ROUTE",new Vector3(24,2.6f,7.86f),Vector3.zero,0.13f);
            Box(root,"Yard approach floor",new Vector3(24,-0.15f,12),new Vector3(4,0.3f,8),floor);
            foreach(float x in new[]{22,26})Wall(root,"Yard approach wall",new Vector3(x,0,12),new Vector3(0.22f,3.6f,8));
            Box(root,"Yard approach roof",new Vector3(24,3.7f,12),new Vector3(4.2f,0.2f,8),cream);
            OpenControlGate(root,new Vector3(24,0,8));OpenControlGate(root,new Vector3(24,0,15.7f));
            LightAt(root,new Vector3(23.4f,2.9f,4),new Color(1,0.88f,0.67f),1.9f,8,false);
            LightAt(root,new Vector3(24,2.9f,11.6f),new Color(1,0.88f,0.67f),1.5f,6,false);
        }
        private void OpenControlGate(Transform root,Vector3 centre)
        {
            foreach(float side in new[]{-1f,1f})
            {
                Box(root,"Control gate post",centre+new Vector3(side*1.4f,1.4f,0),new Vector3(0.09f,2.8f,0.09f),iron);
                for(int i=0;i<5;i++)Box(root,"Open gate bars",centre+new Vector3(side*1.4f,1.3f,0.12f+i*0.26f),new Vector3(0.04f,2.6f,0.04f),cellSteel,false);
                foreach(float y in new[]{0.2f,1.3f,2.6f})Box(root,"Open gate rail",centre+new Vector3(side*1.4f,y,0.66f),new Vector3(0.05f,0.05f,1.3f),cellSteel,false);
            }
            Box(root,"Gate top rail",centre+Vector3.up*2.85f,new Vector3(2.9f,0.09f,0.09f),iron,false);
        }

        private void DiningRoom()
        {
            var root=new GameObject("Dining hall 22 x 18 m").transform;root.SetParent(transform,false);
            Box(root,"Dining floor",new Vector3(29,-0.15f,-8),new Vector3(22,0.3f,18),floor);
            foreach(float x in new[]{18,40})Wall(root,"Dining end wall",new Vector3(x,0,-8),new Vector3(0.2f,5.0f,18));
            Wall(root,"Dining far wall",new Vector3(29,0,-17),new Vector3(22,5.0f,0.2f));
            Wall(root,"Dining short wall",new Vector3(34,0,1),new Vector3(12,5.0f,0.2f));
            Box(root,"Dining roof",new Vector3(29,5.1f,-8),new Vector3(22,0.2f,18),cream);
            foreach(float x in new[]{20.5f,27.5f,31.5f,36.0f})for(int row=0;row<3;row++)
            {
                Vector3 p=new Vector3(x,0,-2.5f-row*4.5f);
                Box(root,"Canteen table",p+Vector3.up*0.8f,new Vector3(1.45f,0.08f,3.0f),wood);
                foreach(float side in new[]{-1f,1f})
                {
                    Box(root,"Bench",p+new Vector3(side*0.96f,0.45f,0),new Vector3(0.38f,0.1f,3.0f),wood);
                    foreach(float z in new[]{-1.15f,1.15f})Box(root,"Bench foot",p+new Vector3(side*0.96f,0.2f,z),new Vector3(0.06f,0.4f,0.06f),iron,false);
                }
                Cylinder(root,"Tin plate",p+new Vector3(0.1f,0.85f,0.45f),new Vector3(0.12f,0.01f,0.12f),iron,false);
            }
            Box(root,"Serving counter",new Vector3(29,1.0f,-15.5f),new Vector3(15,0.16f,0.85f),white);
            for(int i=0;i<3;i++)LightAt(root,new Vector3(21+i*7,4.3f,-7),new Color(1,0.89f,0.68f),2.0f,11,false);
        }

        private void Yard()
        {
            var root=new GameObject("Recreation yard").transform;root.SetParent(transform,false);
            Box(root,"Yard floor 32 x 24 m",new Vector3(30,-0.15f,28),new Vector3(32,0.3f,24),floor);
            foreach(float x in new[]{14,46})Wall(root,"Yard perimeter",new Vector3(x,0,28),new Vector3(0.3f,5.2f,24));
            Wall(root,"Yard north wall",new Vector3(30,0,40),new Vector3(32,5.2f,0.3f));
            Wall(root,"Yard south west wall",new Vector3(18.25f,0,16),new Vector3(8.5f,5.2f,0.3f));
            Wall(root,"Yard south east wall",new Vector3(35.75f,0,16),new Vector3(20.5f,5.2f,0.3f));
            // Stepped seating gives the yard a recognisable end and a place for social groups.
            for(int i=0;i<4;i++)Box(root,"Yard seating tier",new Vector3(30,(i+1)*0.16f,36.2f+i*0.85f),new Vector3(24,(i+1)*0.32f,0.88f),floor);
            for(int i=0;i<32;i++)foreach(float z in new[]{16,40})
                Line(root,"Fence upright",new Vector3(14.5f+i,5.2f,z),new Vector3(14.5f+i,6.2f,z),0.018f,iron,false);
            foreach(float z in new[]{16,40})foreach(float y in new[]{5.5f,5.8f,6.1f})
                Line(root,"Security wire",new Vector3(14,y,z),new Vector3(46,y,z),0.009f,iron,false);
            foreach(float x in new[]{16,44})
            {
                Box(root,"Yard bench",new Vector3(x,0.5f,25),new Vector3(0.5f,0.12f,4),wood);
                foreach(float z in new[]{23.5f,26.5f})Box(root,"Bench support",new Vector3(x,0.24f,z),new Vector3(0.2f,0.48f,0.1f),iron,false);
            }
            foreach(float z in new[]{19,34})Box(root,"Court paint",new Vector3(32,0.012f,z),new Vector3(15,0.01f,0.05f),white,false);
            foreach(float x in new[]{24.5f,39.5f})Box(root,"Court paint",new Vector3(x,0.012f,26.5f),new Vector3(0.05f,0.01f,15),white,false);
            Box(root,"Court centre line",new Vector3(32,0.012f,26.5f),new Vector3(15,0.01f,0.05f),white,false);
            Sign(root,"RECREATION YARD",new Vector3(24,2.8f,16.17f),new Vector3(0,180,0),0.14f);
            Box(root,"Ocean backdrop",new Vector3(15,-5,20),new Vector3(210,0.2f,180),sea,false);
            for(int i=0;i<4;i++)
            {
                float x=46+(i%2)*3,z=8+(i/2)*3;
                Line(root,"Water tower leg",new Vector3(x,-2,z),new Vector3(x,8,z),0.12f,iron,false);
            }
            Cylinder(root,"Water tower",new Vector3(47.5f,9,9.5f),new Vector3(2.3f,1.5f,2.3f),green,false);
            Cylinder(root,"Tower cap",new Vector3(47.5f,10.5f,9.5f),new Vector3(2.4f,0.12f,2.4f),iron,false);
        }
    }
}
