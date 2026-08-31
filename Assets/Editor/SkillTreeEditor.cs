using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
[CustomEditor(typeof(SkillNode))]
class SkillTreeEditor : Editor{
     NodeSide side;
    public override void OnInspectorGUI(){
        SkillNode node = (SkillNode)target;
        DrawDefaultInspector();
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Skill Tree Tools",EditorStyles.boldLabel);
        side = (NodeSide)EditorGUILayout.EnumPopup("Side",side);
        if(GUILayout.Button("Create Connected Skill Node")) CreateSkillNode(node,side);

    }
    private void CreateSkillNode(SkillNode node , NodeSide side){
        GameObject newNode = Instantiate(node.GameObject());
        newNode.name = node.GameObject().name+" Child";
        Vector3 direction;
        switch(side){
            case NodeSide.Up:
                direction=Vector3.up;
                break;
            case NodeSide.Down:
                direction=Vector3.down;
                break;
            case NodeSide.Left:
                direction=Vector3.left;
                break;
            case NodeSide.Right:
                direction=Vector3.right;
                break;
            default:
                direction=Vector3.up;
                break;
        }
        newNode.transform.position = node.GameObject().transform.position +  direction * 2f;

    }
    public enum NodeSide{
        Up,
        Down,
        Left,
        Right
    }
}