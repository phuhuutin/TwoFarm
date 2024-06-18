// In Scripts/BoundingBox.cs
using UnityEngine;

namespace CustomColliders
{
    public class BoundingBox
    {
        public Vector2 position;
        public Vector2 size;

        public Vector2 offset;

        public BoundingBox(Vector2 position, Vector2 size)
        {   
           

            this.position = position + new Vector2(-size.x / 2f, -size.y / 2f);
            this.size = size;
        }

         public BoundingBox(Vector2 position, Vector2 size, Vector2 offset)
        {   
           
            this.offset = offset;
            this.position = position + new Vector2(-size.x / 2f, -size.y / 2f) + offset;
            this.size = size;
        }

        public bool Intersects(BoundingBox other)
        {
            return !(other.position.x > position.x + size.x ||
                     other.position.x + other.size.x < position.x ||
                     other.position.y > position.y + size.y ||
                     other.position.y + other.size.y < position.y);
        }

        public Vector2 GetPenetrationDepth(BoundingBox other)
        {
            float overlapX = Mathf.Min(position.x + size.x, other.position.x + other.size.x) - Mathf.Max(position.x, other.position.x);
            float overlapY = Mathf.Min(position.y + size.y, other.position.y + other.size.y) - Mathf.Max(position.y, other.position.y);

            return new Vector2(overlapX, overlapY);
        }

        public void UpdatePosition(Vector2 newPosition)
        {   
           // this.position = newPosition + new Vector2(-size.x / 2f, -size.y / 2f);
          this.position = newPosition - size/2;
        }

        public void UpdatePosition(Vector2 newPosition, bool isFlipped)
        {   
         
         //   this.position = newPosition + (isFlipped ? new Vector2(size.x / 2f, -size.y / 2f - offset.y) : new Vector2(-size.x / 2f, -size.y / 2f + offset.y) ) - size/2;
            this.position = newPosition + (isFlipped ?  new Vector2(0, -size.y / 2f) - offset : offset )  - size/2  ;


        }



        public void UpdateSize(Vector2 newSize)
        {
            size = newSize;
        }

        public void DrawDebug()
        {   

             // Draw a cross at the current position
    float crossSize = 0.1f; // Adjust the size of the cross as needed
    Debug.DrawLine(new Vector3(position.x - crossSize, position.y, 0), new Vector3(position.x + crossSize, position.y, 0), Color.blue);
    Debug.DrawLine(new Vector3(position.x, position.y - crossSize, 0), new Vector3(position.x, position.y + crossSize, 0), Color.blue);


            Debug.DrawLine(new Vector3(position.x, position.y, 0), new Vector3(position.x + size.x, position.y, 0), Color.red);
            Debug.DrawLine(new Vector3(position.x, position.y, 0), new Vector3(position.x, position.y + size.y, 0), Color.red);
            Debug.DrawLine(new Vector3(position.x + size.x, position.y, 0), new Vector3(position.x + size.x, position.y + size.y, 0), Color.red);
            Debug.DrawLine(new Vector3(position.x, position.y + size.y, 0), new Vector3(position.x + size.x, position.y + size.y, 0), Color.red);
        }
    }
}
