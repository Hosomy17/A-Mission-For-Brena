using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[ExecuteInEditMode]
public class PreviewCulling : MonoBehaviour {

#if UNITY_EDITOR

    static Camera sceneReferenceCamera;

    Vector3 pos;
    Quaternion rot;
    float near;
    float far;

    Camera sceneCamera{
        get{
            return GetSceneCamera();
        }
    }

    void OnEnable() {
        if (sceneCamera && gameObject != sceneCamera.gameObject) {
            sceneReferenceCamera = GetComponent<Camera>();
            UpdateSceneCamera();
        }
    }

    void OnDisable() {
        if (sceneCamera && gameObject != sceneCamera.gameObject) {
            CleanSceneCamera();
            sceneReferenceCamera = null;
        }
    }

    private Camera GetSceneCamera() {
        SceneView SceneView = EditorWindow.GetWindow<SceneView>();
        return SceneView.camera;
    }

    private void UpdateSceneCamera() {
        //ad a PreviewCulling script to the scene camera if not done already
        if (sceneCamera && !sceneCamera.GetComponent<PreviewCulling>()) {
            sceneCamera.gameObject.AddComponent<PreviewCulling>();
        }
    }

    void CleanSceneCamera() {
        if (sceneCamera) {
            //remove the PreviewCulling script from the scene camera
            PreviewCulling pc = sceneCamera.GetComponent<PreviewCulling>();
            if (pc) {
                DestroyImmediate(pc);
            }
        }
    }

    void OnPreCull() {
        //if the scene camera is about to cull, save current transform values and copy from the reference camera
        if (Camera.current == sceneCamera) { 
            if (sceneReferenceCamera) {
                pos = Camera.current.transform.position;
                rot = Camera.current.transform.rotation;
                near = Camera.current.nearClipPlane;
                far = Camera.current.farClipPlane;
                Camera.current.projectionMatrix = sceneReferenceCamera.projectionMatrix;
                Camera.current.transform.position = sceneReferenceCamera.transform.position;
                Camera.current.transform.rotation = sceneReferenceCamera.transform.rotation;
                Camera.current.nearClipPlane = sceneReferenceCamera.nearClipPlane;
                Camera.current.farClipPlane = sceneReferenceCamera.farClipPlane;
            }
        }
    }

    void OnPreRender() {
        //if the scene camera is about to render reset its transform to saved values
        if (Camera.current == sceneCamera) {
            Camera.current.transform.position = pos;
            Camera.current.transform.rotation = rot;
            Camera.current.nearClipPlane = near;
            Camera.current.farClipPlane = far;
            Camera.current.ResetProjectionMatrix();
        }
    }
#endif
}