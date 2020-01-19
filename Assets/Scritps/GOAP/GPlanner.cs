﻿using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);// Copy the dictionary
        this.action = action;
    }

    public Node (Node parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefStates ,GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);// Copy the dictionary
        this.action = action;

        foreach (KeyValuePair<string, int> kvp in beliefStates)
        {
            if(!this.state.ContainsKey(kvp.Key))
                this.state.Add(kvp.Key, kvp.Value);
        }
    }
}

public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction action in actions)
        {
            if(action.IsAchievcable())
                usableActions.Add(action);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), beliefStates.GetStates(), null);//first node

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;

        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else if (leaf.cost < cheapest.cost)
                cheapest = leaf;
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction action in result)
        {
            queue.Enqueue(action);
        }

        Debug.Log("The Plan is: ");
        foreach (GAction action in queue)
        {
            Debug.Log($"Q: {action.actionName}");
        }

        return queue;
    }

    bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                foreach (KeyValuePair<string, int> effect in action.effects)
                {
                    if(!currentState.ContainsKey(effect.Key))
                        currentState.Add(effect.Key, effect.Value);
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);

                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }

    List<GAction> ActionSubset (List<GAction> actions, GAction actionToRemove)
    {
        List<GAction> subset = new List<GAction>();

        foreach (GAction action in actions)
        {
            if(!action.Equals(actionToRemove))
                subset.Add(action);
        }

        return subset;
    }
}