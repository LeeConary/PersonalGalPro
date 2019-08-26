using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourWithTree : MonoBehaviour 
{
    public class Node
    {
        public string interestingValue = "value";
        public List<Node> children = new List<Node>();
    }

    [Serializable]
    public struct SerializableNode
    {
        public string interestingValue;
        public int childCount;
        public int indexOfFirstChild;
    }

    Node root = new Node();
    public List<SerializableNode> serializableNodes;

    public void OnBeforeSerialize()
    {
        serializableNodes.Clear();
        AddNodesToSerializeField(root);
    }

    void AddNodesToSerializeField(Node node)
    {
        var serializeNode = new SerializableNode()
        {
            interestingValue = node.interestingValue,
            childCount = node.children.Count,
            indexOfFirstChild = serializableNodes.Count + 1

        };

        serializableNodes.Add(serializeNode);
        foreach(var child in node.children)
        {
            AddNodesToSerializeField(child);
        }
    }

    public void OnAfterDeserialize()
    {
        if (serializableNodes.Count > 0)
        {
            root = ReadNodeFromSerializedNodes(0);
        }
        else
        {
            root = new Node();
        }
    }

    Node ReadNodeFromSerializedNodes(int index)
    {
        var serializedNode = serializableNodes[index];
        var children = new List<Node>();
        for (int i = 0; i != serializableNodes.Count; i++)
        {
            children.Add(ReadNodeFromSerializedNodes(serializedNode.indexOfFirstChild + 1));
        }
        return new Node()
        {
            interestingValue = serializedNode.interestingValue,
            children = children
        };
    }

    private void OnGUI()
    {
        DisPlay(root);
    }

    void DisPlay(Node node)
    {
        GUILayout.Label("Value: ");
        node.interestingValue = GUILayout.TextField(node.interestingValue, GUILayout.Width(200));

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginVertical();

        foreach (var item in node.children)
        {
            if (GUILayout.Button("Add child"))
            {
                node.children.Add(new Node());
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}
