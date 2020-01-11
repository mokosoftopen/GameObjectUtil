using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectUtil
{

    public static T Find<T>(string objname) where T : class
    {
        return Find<T>(objname, false, null);
    }

    public static T Find<T>(string objname, bool isChild, Transform t) where T : class
    {
        string search_obj = "";
        string search_next = "";
        if ( objname.Contains("/") ) {
            // 要素を分解する
            string[] kaiso = objname.Split('/');
            search_obj = kaiso[0];
            for ( int i = 1; i <= kaiso.Length-1; i++ ) {
                if ( search_next.Length == 0 ) {
                    search_next += kaiso[i];
                } else {
                    search_next += "/"+kaiso[i];
                }
            }

        } else {
            search_obj = objname;
        }

        GameObject s = null;
        if ( isChild ) {
            s = FindChildRe(search_obj, t);
        } else {
            s = GameObject.Find(search_obj);
            if ( s == null ) {
                // 存在しないか、非アクティブオブジェクトだと見つからない
                for ( int i = 0; i < SceneManager.sceneCount; i++ ) {
                    Scene tmp = SceneManager.GetSceneAt(i);
                    GameObject[] roots = tmp.GetRootGameObjects();
                    foreach ( GameObject rg in roots){
                        Debug.Log("search "+rg.name);
                        s = FindChildRe(search_obj, rg.transform);
                        if ( s != null ) {
                            break;
                        }
                    }
                    if ( s != null ) {
                        break;
                    }
                }
            }
        }

        if ( s == null ) {
            throw new System.Exception("Objectが見つかりません");
        }
      
        if ( search_next.Length > 0 ) {
            return Find<T>(search_next, true, s.transform);;
        } else {
            if(typeof(T) == typeof(GameObject)){
                return (T)(object)s;
            } else {
                return s.GetComponent<T>();
            }
        }

    }

    public static GameObject FindChildRe(string search_obj, Transform t)
    {
        if ( t.childCount <= 0 ) return null;
        GameObject s = null;
        for ( int i = 0; i < t.childCount; i++){
            Transform _t = t.GetChild(i);
            if ( _t.gameObject.name.CompareTo(search_obj) == 0 ){
                s = _t.gameObject;
                break;
            } else {
                if ( _t.childCount > 0 ) {
                   s = FindChildRe(search_obj, _t);
                   if ( s != null ) break; 
                }
            }
        }
        return s;
    }
}
