﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Item
{
    public Cell Cell { get; private set; }

    public Transform View { get; private set; }


    public virtual void SetView()
    {
        string prefabname = GetPrefabName();

        if (!string.IsNullOrEmpty(prefabname))
        {
            GameObject prefab = Resources.Load<GameObject>(prefabname);
            if (prefab)
            {
                //View = GameObject.Instantiate(prefab).transform;
                View = SimplePooler.instance.Spawn(prefab.name, prefab).transform;

                CharacterSkin skin = Resources.Load<CharacterSkin>(Constants.CHARACTER_SKIN_PATH);
                if (skin)
                {
                    Sprite sprite = skin.atlas.GetSprite(prefab.name);
                    if (sprite)
                        View.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                }
            }
        }
    }

    protected virtual string GetPrefabName() { return string.Empty; }

    public virtual void SetCell(Cell cell)
    {
        Cell = cell;
    }

    internal void AnimationMoveToPosition()
    {
        if (View == null) return;

        View.DOMove(Cell.transform.position, 0.2f);
    }

    public void SetViewPosition(Vector3 pos)
    {
        if (View)
        {
            View.position = pos;
        }
    }

    public void SetViewRoot(Transform root)
    {
        if (View)
        {
            View.SetParent(root);
        }
    }

    public void SetSortingLayerHigher()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 1;
        }
    }


    public void SetSortingLayerLower()
    {
        if (View == null) return;

        SpriteRenderer sp = View.GetComponent<SpriteRenderer>();
        if (sp)
        {
            sp.sortingOrder = 0;
        }

    }

    internal void ShowAppearAnimation()
    {
        if (View == null) return;

        Vector3 scale = View.localScale;
        View.localScale = Vector3.one * 0.1f;
        View.DOScale(scale, 0.1f);
    }

    internal virtual bool IsSameType(Item other)
    {
        return false;
    }

    internal virtual void ExplodeView()
    {
        if (View)
        {
            View.DOScale(0.1f, 0.1f).OnComplete(
                () =>
                {
                    //GameObject.Destroy(View.gameObject);
                    SimplePooler.instance.Despawn(View.gameObject.name, View.gameObject);
                    View = null;
                }
                );
        }
    }



    internal void AnimateForHint()
    {
        if (View)
        {
            View.DOPunchScale(View.localScale * 0.1f, 0.1f).SetLoops(-1);
        }
    }

    internal void StopAnimateForHint()
    {
        if (View)
        {
            View.DOKill();
        }
    }

    internal void Clear()
    {
        Cell = null;

        if (View)
        {
            View.gameObject.SetActive(false);
            //GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}
