using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LylekGames {
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class CombineMeshes : MonoBehaviour {
    
		private Matrix4x4 myMatrix;
        [HideInInspector]
        public MeshFilter myMeshFilter;
        private MeshRenderer myMeshRenderer;
        private MeshFilter[] meshFilters;
        private MeshRenderer[] meshRenderers;

        public void Start()
        {
            myMeshFilter = GetComponent<MeshFilter>();
            myMeshRenderer = GetComponent<MeshRenderer>();
        }
        public void EnableMeshPreserveColliders()
        {
            myMatrix = transform.worldToLocalMatrix;
            CombineInstance[] combine;
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
            meshFilters = GetComponentsInChildren<MeshFilter>();
            combine = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].sharedMesh != null)
                {
                    combine[i].mesh = meshFilters[i].sharedMesh;
                    combine[i].transform = myMatrix * meshFilters[i].transform.localToWorldMatrix;
                }
            }
            foreach(MeshRenderer meshRenderer in meshRenderers)
                if(meshRenderer.gameObject != this.gameObject)
                    meshRenderer.enabled = false;
            myMeshFilter.mesh = new Mesh();
            myMeshFilter.sharedMesh.CombineMeshes(combine);
            myMeshRenderer.material = meshFilters[1].GetComponent<Renderer>().sharedMaterial;
            gameObject.isStatic = true;
        }
        public void EnableMeshNewCollider() {
			myMatrix = transform.worldToLocalMatrix;
            CombineInstance[] combine;
            meshFilters = GetComponentsInChildren<MeshFilter>();
            combine = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++) {
				if (meshFilters[i].sharedMesh != null) {
					combine[i].mesh = meshFilters[i].sharedMesh;
					combine[i].transform = myMatrix * meshFilters[i].transform.localToWorldMatrix;
                    meshFilters[i].gameObject.SetActive(false);
				}
			}
            myMeshFilter.mesh = new Mesh();
            myMeshFilter.sharedMesh.CombineMeshes(combine);
            myMeshRenderer.material = meshFilters[1].GetComponent<Renderer>().sharedMaterial;
            gameObject.AddComponent<MeshCollider>();
            gameObject.isStatic = true;
        }

		public void DisableMesh() {
			for(int i = 0; i < meshFilters.Length; i++) {
                meshFilters[i].gameObject.SetActive(true);
            }
            if(meshRenderers != null)
                if(meshRenderers.Length > 0)
                    foreach (MeshRenderer meshRenderer in meshRenderers)
                        meshRenderer.enabled = true;
            myMeshFilter.mesh = null;
            myMeshRenderer.material = null;
            if (GetComponent<Collider>())
                DestroyImmediate(gameObject.GetComponent<Collider>());
        }
	}
}
