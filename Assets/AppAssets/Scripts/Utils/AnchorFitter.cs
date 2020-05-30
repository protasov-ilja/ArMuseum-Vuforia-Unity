using UnityEditor;
using UnityEngine;
public static class AnchorFitter
{
	[MenuItem("CONTEXT/RectTransform/Fit Anchors")]
	public static void FitAnchors(MenuCommand command)
	{
		RectTransform rect = (RectTransform)command.context;
		RectTransform parent = rect.parent as RectTransform;
		if(parent == null)
		{
			return;
		}

		Vector2 offsetMin = rect.offsetMin;
		Vector2 offsetMax = rect.offsetMax;
		Vector2 anchorMin = rect.anchorMin;
		Vector2 anchorMax = rect.anchorMax;
		Vector2 parentSize = parent.rect.size;

		anchorMin = new Vector2(anchorMin.x + (offsetMin.x / parentSize.x), anchorMin.y + (offsetMin.y / parentSize.y));
		anchorMax = new Vector2(anchorMax.x + (offsetMax.x / parentSize.x), anchorMax.y + (offsetMax.y / parentSize.y));

		Undo.RecordObject(rect, "Fit anchors to transform");
		rect.anchorMin = anchorMin;
		rect.anchorMax = anchorMax;
		rect.offsetMin = Vector2.zero;
		rect.offsetMax = new Vector2(1, 1);
		rect.pivot = new Vector2(0.5f, 0.5f);
	}
}