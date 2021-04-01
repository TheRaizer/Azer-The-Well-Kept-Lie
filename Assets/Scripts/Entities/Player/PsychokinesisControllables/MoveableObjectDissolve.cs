using UnityEngine;

namespace Azer.EntityComponents
{
    public class MoveableObjectDissolve : MonoBehaviour
    {
        private Material dissolveMaterial;

        [SerializeField] private Collider2D _collider = null;

        public bool IsDissolving { get; private set; }
        public bool IsResolving { get; private set; }
        public bool HasDissolved { get; private set; }
        public bool HasResolved { get; set; }


        private float fade = 1f;

        void Awake()
        {
            dissolveMaterial = GetComponent<SpriteRenderer>().material;
        }

        private void Update()
        {
            Dissolve();
            Resolve();
        }

        private void Dissolve()
        {
            if (IsDissolving)
            {
                HasDissolved = false;

                fade -= Time.deltaTime;

                if (fade <= 0)
                {
                    HasDissolved = true;
                    IsDissolving = false;
                    fade = 0;
                }
                dissolveMaterial.SetFloat("_Fade", fade);
            }
        }

        private void Resolve()
        {
            if(IsResolving)
            {
                HasResolved = false;

                fade += Time.deltaTime;

                if (fade >= 1)
                {
                    HasDissolved = false;
                    HasResolved = true;
                    IsResolving = false;
                    fade = 1;
                }

                dissolveMaterial.SetFloat("_Fade", fade);
            }
        }

        public void ResetFade()
        {
            fade = 1f;
            HasDissolved = false;
            HasResolved = false;
        }
        public void StartDissolving()
        {
            _collider.enabled = false;
            IsDissolving = true;
        }
        public void StartResolving()
        {
            _collider.enabled = true;
            IsResolving = true;
        }

    }
}
