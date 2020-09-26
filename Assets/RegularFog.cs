using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IStreamYouScream
{
    public class RegularFog : MonoBehaviour
    {
        [SerializeField] bool InitialIctive;
        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value == IsActive)
                {
                    return;
                }
                _isActive = value;
                UpdatePieces();
            }
        }
        public ActionOpenDoor Door;
        public RegularFogPiece Piece;
        private List<RegularFogPiece> pieces = new List<RegularFogPiece>();
        public GameObject LeftBottom;
        public GameObject RightTop;
        void Start()
        {
            if (Door != null)
            {
                BindDoorEvents();
            }

            Vector3 position = LeftBottom.transform.position;
            Vector3 max = RightTop.transform.position;

            RegularFogPiece _piece = Instantiate(Piece, position, Quaternion.identity);
            Vector3 size = _piece.GetComponent<Renderer>().bounds.size;
            Destroy(_piece);

            float x = position.x + size.x / 2;
            while (x < max.x)
            {
                float y = position.y + size.y / 2;
                while (y < max.y)
                {
                    RegularFogPiece piece = Instantiate(Piece, new Vector3(x, y, 0), Quaternion.identity, transform);
                    y += size.y * .65f;
                    pieces.Add(piece);
                }
                x += size.x * .65f;
            }

            IsActive = InitialIctive;
        }

        private void BindDoorEvents()
        {
            Door.OnOpened.AddListener(() => IsActive = true);
            Door.OnClosed.AddListener(() => IsActive = false);
        }

        private void UpdatePieces()
        {
            foreach (Transform child in this.transform)
            {
                RegularFogPiece piece = child.GetComponent<RegularFogPiece>();
                if (piece != null)
                {
                    piece.IsActive = IsActive;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 l = LeftBottom.transform.position;
            Vector3 r = RightTop.transform.position;

            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube((l + r) / 2, r - l);
        }
    }
}