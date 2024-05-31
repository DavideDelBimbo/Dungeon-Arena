using UnityEngine;
using System.Collections.Generic;
using DungeonArena.Interfaces;

namespace DungeonArena.Utils {
    public static class MovementUtils {
        // Check if there's enough space around the agent to move.
        public static bool HasSufficientSpace(Vector2 movement, IAgent agentToMove, List<IAgent> agents, float separationDistance) {
            // If the agent is not moving, there's enough space.
            if (movement == Vector2.zero) {
                return true;
            }

            // Get the direction of the movement.
            Vector2 movementDirection = movement.normalized;

            // Check if any agent is in the way.
            foreach (IAgent agent in agents) {
                // Skip if the agent is null.
                if (agent == null || agent.Transform == null) {
                    continue;
                }

                // Calculate the distance from the agent.
                Vector2 distanceFromOtherAgent = agent.Transform.position - agentToMove.Transform.position;

                // Check if the agent is in the direction of movement and within separation distance.
                if (Vector2.Dot(distanceFromOtherAgent.normalized, movementDirection) > 0.9f && distanceFromOtherAgent.magnitude < separationDistance) {
                    // Not enough space to move.
                    return false;
                }
            }

            // There's enough space around the agent to move.
            return true;
        }
    }
}
