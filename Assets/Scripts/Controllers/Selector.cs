using UnityEngine;
using UnityEngine.EventSystems;

namespace GunduzDev
{
    public class Selector : MonoSingleton<Selector>
    {
        private Camera _cam;

        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            _cam = Camera.main;
        }

        public Vector3 GetCurTilePosition()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return new Vector3(0, -99f, 0);
            }

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            float rayOut = 0f;

            if (plane.Raycast(ray, out rayOut))
            {
                Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(.5f,0,.5f);
                newPos = new Vector3(Mathf.CeilToInt(newPos.x),0,Mathf.CeilToInt(newPos.z));
                return newPos;
            }
            
            return new Vector3(0,-99,0);
        }
    }
}
