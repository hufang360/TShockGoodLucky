进服 和 重生时 给予 buff。

可配置多个buff，以及时长。

配置修改后，可用自带的 `/reload` 指令进行刷新。

<br>

配置文件示例：

给予 好运 buff 52分钟 以及 发光 buff 不限时

```json
{
  "buff": [
    {
      "id": 257,
      "seconds": 3120
    },
    {
      "id": 11,
      "seconds": -1
    }
  ]
}
```