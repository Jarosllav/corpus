using UnityEngine;

namespace Corpus.Extensions
{
    public static class GizmosExtensions
    {
        public static void DrawWireCapsule(Vector3 point1, Vector3 point2, float radius)
        {
            var axis = point2 - point1;
            if (axis.sqrMagnitude < 0.0001f)
            {
                Gizmos.DrawWireSphere(point1, radius);
                return;
            }

            var up = axis.normalized;
            var perpendicular = Vector3.Cross(up, Vector3.up).sqrMagnitude > 0.001f
                ? Vector3.Cross(up, Vector3.up)
                : Vector3.Cross(up, Vector3.right);
            var rotation = Quaternion.LookRotation(perpendicular, up);

            var right = rotation * Vector3.right;
            var forward = rotation * Vector3.forward;

            Gizmos.DrawWireSphere(point1, radius);
            Gizmos.DrawWireSphere(point2, radius);

            Gizmos.DrawLine(point1 + right * radius, point2 + right * radius);
            Gizmos.DrawLine(point1 - right * radius, point2 - right * radius);
            Gizmos.DrawLine(point1 + forward * radius, point2 + forward * radius);
            Gizmos.DrawLine(point1 - forward * radius, point2 - forward * radius);

            DrawHalfCircle(point2, up, right, radius, false);
            DrawHalfCircle(point2, up, forward, radius, false);
            DrawHalfCircle(point1, up, right, radius, true);
            DrawHalfCircle(point1, up, forward, radius, true);
        }

        private static void DrawHalfCircle(Vector3 center, Vector3 up, Vector3 axis, float radius, bool flip)
        {
            var segments = 16;
            var side = Vector3.Cross(up, axis).normalized;
            var dir = flip ? -1f : 1f;
            var prev = center + up * radius * dir;

            for (var i = 1; i <= segments; i++)
            {
                var angle = i * Mathf.PI / segments;
                var next = center + (Mathf.Cos(angle) * up * dir + Mathf.Sin(angle) * side) * radius;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
        }
    }
}