# AR數學動作遊戲

一個結合擴增實境（AR）和手勢辨識的互動式數學遊戲，專為小朋友設計。

## 🎮 功能特色

- **雙模式支援**：
  - 📷 攝影機模式：使用手勢指向答案（需要攝影機）
  - 🖱️ 點擊模式：直接點擊答案（無攝影機也可玩）
  
- **智慧答題系統**：
  - 十位數加減法題目（10-99）
  - 答錯可重試，最多 3 次機會
  - 答錯 3 次後顯示正確答案
  
- **完整記錄功能**：
  - 記錄所有答題歷史
  - 顯示題目、答案、時間戳記
  - 區分答對/答錯

- **響應式設計**：
  - 支援手機、平板、電腦
  - 自動適應不同螢幕尺寸

## 🚀 快速開始

### 方法一：直接開啟（最簡單）

1. 直接雙擊 `index.html` 檔案
2. 用瀏覽器開啟（建議使用 Chrome 或 Edge）
3. 允許攝影機權限（如果有）
4. 開始遊戲！

### 方法二：使用本地伺服器（推薦）

攝影機功能在某些瀏覽器需要 HTTPS 或本地伺服器才能運作。

#### 使用 Python（如果已安裝）
bash
# Python 3.x
cd D:\VibeCoding\MathGame
python -m http.server 8000

# 然後在瀏覽器開啟：
http://localhost:8000

使用 Node.js（如果已安裝）
bash# 安裝 http-server
npm install -g http-server

# 啟動伺服器
cd D:\VibeCoding\MathGame
http-server

# 然後在瀏覽器開啟顯示的網址
使用 Visual Studio Code（如果已安裝）

用 VS Code 開啟 MathGame 資料夾
安裝 "Live Server" 擴充功能
在 index.html 上按右鍵 → "Open with Live Server"

### 方法三：部署到網路
使用 GitHub Pages（免費）

在 GitHub 建立新的 repository
上傳 index.html
在 Settings → Pages 啟用 GitHub Pages
取得網址，即可在任何裝置開啟

使用 Netlify（免費）

前往 Netlify
拖曳整個 MathGame 資料夾到網站
自動部署，取得網址

# 📱 使用說明
攝影機模式（手勢辨識）

確保攝影機權限已開啟
將手伸出，確保手部完整出現在畫面中
用食指指向螢幕四個角落的答案
保持手勢 1 秒鐘確認選擇
看到黃色邊框表示正在選擇

手勢對應：

👆 指向左上 = 選擇左上答案
👆 指向右上 = 選擇右上答案
👆 指向左下 = 選擇左下答案
👆 指向右下 = 選擇右下答案

點擊模式

直接用滑鼠或手指點擊答案即可

答題規則

✅ 答對：得 10 分，進入下一題
❌ 答錯第 1 次：顯示「還有 2 次機會」
❌ 答錯第 2 次：顯示「還有 1 次機會」
❌ 答錯第 3 次：顯示正確答案，進入下一題

🔧 技術需求
瀏覽器支援

✅ Google Chrome（推薦）
✅ Microsoft Edge
✅ Safari（iOS/macOS）
⚠️ Firefox（部分功能可能受限）

系統需求

攝影機模式：

需要攝影機權限
建議使用較新的裝置（近 3 年內）
良好的光線環境


點擊模式：

任何支援 HTML5 的瀏覽器即可



🛠️ 技術架構

前端框架：純 HTML + CSS + JavaScript
手勢辨識：Google MediaPipe Hands
相機控制：MediaPipe Camera Utils
繪圖工具：MediaPipe Drawing Utils

📂 檔案結構
MathGame/
├── index.html          # 主程式（包含所有 HTML/CSS/JS）
└── README.md          # 說明文件
🐛 常見問題
Q: 攝影機無法啟動？
A:

確認瀏覽器有攝影機權限
確認沒有其他程式佔用攝影機
嘗試重新整理頁面
如果無法解決，可使用點擊模式繼續遊戲

Q: 手勢辨識不靈敏？
A:

確保光線充足
確保手部完整出現在畫面中
手指盡量伸直
避免背光

Q: 載入很慢？
A: MediaPipe 需要下載 AI 模型（約 2-5 MB），第一次載入會較慢，之後會快很多。
Q: 手機上可以用嗎？
A: 可以！建議使用較新的手機（近 3 年內），體驗更流暢。
📝 授權
此專案為教育用途，可自由使用和修改。

如需修改程式碼：

編輯 index.html
主要變數在 <script> 標籤內的開頭
可調整的參數：

SELECTION_TIME: 手勢確認時間（預設 1000ms）
MAX_ATTEMPTS: 最大答錯次數（預設 3 次）
題目難度範圍（目前是 10-99）