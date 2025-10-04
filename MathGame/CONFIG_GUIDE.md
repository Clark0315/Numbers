# 設定檔說明文件

## 檔案位置
`config.json` - 遊戲主要設定檔

## 設定項目說明

### 1. game（遊戲基本設定）
```json
{
  "defaultDifficulty": "normal",  // 預設難度：easy, normal, hard
  "selectionTime": 1000,          // 手勢確認時間（毫秒）
  "maxAttempts": 3,               // 每題最大答錯次數
  "scorePerCorrect": 10           // 答對一題的得分
}
```

### 2. difficulty（難度設定）
```json
{
  "easy": {
    "minNum": 0,              // 最小數字
    "maxNum": 9,              // 最大數字
    "label": "容易 (0-9)"      // 顯示標籤
  },
  "normal": {
    "minNum": 0,
    "maxNum": 19,
    "label": "一般 (0-19)"
  },
  "hard": {
    "minNum": 10,
    "maxNum": 99,
    "label": "困難 (10-99)"
  }
}
```

**使用範例：**
- 想要新增「超級困難」模式：可以加入 `"veryhard"` 設定
- 想要調整容易模式為 1-10：修改 `easy.minNum` 為 1，`maxNum` 為 10

### 3. audio（音效設定）
```json
{
  "correctSound": {
    "volume": 3.0,                          // 音量（0.0-5.0，建議不超過3.0）
    "duration": 0.8,                        // 持續時間（秒）
    "frequencies": [523.25, 659.25, 783.99], // 音階頻率（Hz）
    "type": "sine"                          // 波形類型：sine, square, sawtooth, triangle
  },
  "wrongSound": {
    "volume": 2.4,
    "duration": 0.6,
    "frequencies": [300, 200],
    "type": "sawtooth"
  }
}
```

**音效調整建議：**
- 音量太小：增加 `volume` 值
- 音效太短：增加 `duration` 值
- 想要不同音階：修改 `frequencies` 陣列（參考音符頻率表）
- 改變音色：修改 `type`（sine=柔和，sawtooth=粗糙，square=電子音）

**常用音符頻率（Hz）：**
- C4: 261.63
- C5: 523.25
- D5: 587.33
- E5: 659.25
- F5: 698.46
- G5: 783.99
- A5: 880.00

### 4. handTracking（手部追蹤設定）
```json
{
  "maxNumHands": 2,                 // 最大偵測手數
  "modelComplexity": 1,             // 模型複雜度（0=輕量, 1=完整）
  "minDetectionConfidence": 0.5,    // 偵測最低信心值（0.0-1.0）
  "minTrackingConfidence": 0.5,     // 追蹤最低信心值（0.0-1.0）
  "boundaryThreshold": 0.15,        // 邊界警告觸發範圍（畫面比例 0.0-0.5）
  "selectionMargin": 20             // 按鈕選取容錯範圍（像素）
}
```

**調整建議：**
- 手勢不靈敏：降低 `minDetectionConfidence` 和 `minTrackingConfidence`
- 誤觸太多：增加上述信心值
- 邊界警告太早：降低 `boundaryThreshold`
- 按鈕難以觸碰：增加 `selectionMargin`

### 5. ui（介面設定）
```json
{
  "boundaryWarning": {
    "blinkSpeed": 0.25,         // 閃爍速度（秒，越小越快）
    "borderWidth": "15px",      // 邊框寬度
    "borderColor": "red"        // 邊框顏色
  },
  "feedbackDuration": {
    "correct": 1500,            // 答對提示顯示時間（毫秒）
    "wrong": 1500,              // 答錯提示顯示時間（毫秒）
    "wrongFinal": 2500          // 答錯3次顯示正確答案時間（毫秒）
  },
  "instructionHideDelay": 8000  // 遊戲說明自動隱藏時間（毫秒）
}
```

## 如何使用設定檔

### 方法一：直接修改 config.json
1. 用文字編輯器打開 `config.json`
2. 修改想要的數值
3. 儲存檔案
4. 重新載入網頁（需要實作載入功能）

### 方法二：在 index.html 中嵌入設定
目前設定是直接寫在 `index.html` 的 JavaScript 中。如果想要使用外部 `config.json`，需要：

1. 在 `index.html` 的 `<script>` 標籤前加入：
```html
<script>
  // 載入設定檔
  let gameConfig = null;
  fetch('config.json')
    .then(response => response.json())
    .then(config => {
      gameConfig = config;
      console.log('設定檔載入成功', config);
    })
    .catch(error => {
      console.error('設定檔載入失敗，使用預設值', error);
    });
</script>
```

2. 在需要使用設定的地方，改為讀取 `gameConfig`：
```javascript
// 原本
const SELECTION_TIME = 1000;

// 改為
const SELECTION_TIME = gameConfig?.game?.selectionTime || 1000;
```

## 常見調整範例

### 增加遊戲難度
```json
"game": {
  "maxAttempts": 1  // 改為只能錯1次
}
```

### 加快遊戲節奏
```json
"ui": {
  "feedbackDuration": {
    "correct": 800,
    "wrong": 800,
    "wrongFinal": 1500
  }
}
```

### 調整音效更明顯
```json
"audio": {
  "correctSound": {
    "volume": 4.0,  // 更大聲
    "duration": 1.0  // 更長
  }
}
```

### 放寬手勢偵測
```json
"handTracking": {
  "minDetectionConfidence": 0.3,  // 更容易偵測
  "selectionMargin": 40  // 更大的觸碰範圍
}
```

## 注意事項

1. **JSON 格式**：修改時請確保 JSON 格式正確，最後一個項目不能有逗號
2. **數值範圍**：某些數值有合理範圍，超出可能導致異常
3. **備份**：修改前建議先備份原始檔案
4. **測試**：修改後務必測試功能是否正常

## 進階自訂

想要新增難度或修改更多設定，請參考 `index.html` 中的 JavaScript 程式碼。主要函數：
- `generateQuestion()` - 題目生成
- `playCorrectSound()` / `playWrongSound()` - 音效播放
- `checkBoundary()` - 邊界檢測
- `detectSelection()` - 手勢選擇
