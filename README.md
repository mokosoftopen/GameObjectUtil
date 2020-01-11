# GameObjectUtil


以下のヒエラルキー構造の時


━ Area

　　┣ Item1
  
　　┣ Item2
  
 　　　┗ Item4
    
　　┗ Item3
  
━ Util

　　┗ Setting
  
━ Canvas

　　┣ Right
  
 　　　┗ Item
    
　　┗ Left
  
　　　　┗ Item
    
   

Canvasオブジェクトは非アクティブになっている

UtilにScriptを設置し、そこからオブジェクトを取得するとき



```c#:sample
  var item4 = GameObjectUtil.Find<GameObject>("Item4"); //全体からユニークなアイテムを探す
  var setting = GameObjectUtil.Find<GameObject>("Setting", true, transform); //子から探す
  var item = GameObjectUtil.Find<GameObject>("Left/Item"); //非アクティブオブジェクトも探す
  　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　// Item というオブジェクトはユニークではないので、親の Left との階層構造を追記することでユニーク性をもたせる

  var btn = GameObjectUtil.Find<Button>("Item3"); //Item3のComponentを直接取得する
  
```
 
 という感じで使える
　
