using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class RegularFog : MonoBehaviour
    {
        public GameObject Piece;
        public GameObject LeftBottom;
        public GameObject RightTop;
        void Start()
        {
            Vector3 position = LeftBottom.transform.position;
            Vector3 max = RightTop.transform.position;

            GameObject piece = Instantiate(Piece, position, Quaternion.identity, transform);
            Vector3 size = piece.GetComponent<Renderer>().bounds.size;
            Destroy(piece);

            float x = position.x;
            while (x < max.x)
            {
                float y = position.y;
                while (y < max.y)
                {
                    Instantiate(Piece, new Vector3(x, y, 0), Quaternion.identity, transform);
                    y += size.y * .65f;
                }
                x += size.x * .65f;
            }
        }
    }
}