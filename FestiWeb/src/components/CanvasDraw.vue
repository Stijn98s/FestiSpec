<template>
    <v-container flex center   class="text-xs-center">
        <div class="canvas-wrapper" ref="canvasWrapper">
            <div class="draw-area">
                <canvas id="canvas" ref="canvas" :width="width" :height="height"></canvas>
                <canvas id="cursor" ref="cursor" :width="width" :height="height"></canvas>
            </div>
            <input ref="picInput" id="inp_img" name="img" type="hidden" value="">
        </div>
        <v-btn @click="clearCanvas">
            clear
        </v-btn>
        <v-btn @click="showColorPalette()">
            Palette
        </v-btn>

        <v-btn color="success" v-on:click="upload">
            <div v-if="!isLoading">Verstuur</div>
            <v-progress-circular v-else :indeterminate="true"></v-progress-circular>
        </v-btn>
    </v-container>
</template>

<script>
    import * as axios from 'axios'
    import {imageServerUrl} from "../services/ApiServices";

    export default {
        name: 'CanvasDraw',
        props: {
            bgUrl: {
                type: String,
                default: ''
            },
            answerUrl: {
                type: String,
                default: null
            },
            brushSize: {
                type: Number,
                default: 12
            },
            height: {
                type: Number,
                default: 480
            },
            outputName: {
                type: String,
                default: 'canvas'
            }
        },
        data() {
            return {
                canvasContext: null,
                cursorContext: null,
                isDrawing: false,
                lastX: 0,
                lastY: 0,
                isLoading: false,
                width: 480,
                tools: [
                    {
                        name: 'Pencil',
                        color: '#000000'
                    },
                    {
                        name: 'Eraser'
                    }
                ],
                selectedToolIdx: 0
            }
        },
        mounted() {
            this.setCanvas()
            this.bindEvents()
        },
        methods: {
            clearCanvas() {
                var background = new Image()
                this.width = window.innerWidth * 0.65;
                if (window.innerWidth < 500) this.width = window.innerWidth * 0.8;
                this.$refs.canvasWrapper.style.width = `${this.width + 30}px`
                this.$refs.canvasWrapper.style.height = `${this.height}px`
                background.src = this.bgUrl
                background.crossOrigin = 'Anonymous'
                background.onload = () => {
                    drawImageProp(this.canvasContext, background, 0, 0, this.width, this.height)
                }
            },
            setCanvas() {
                this.width = window.innerWidth * 0.65;
                if (window.innerWidth < 500) this.width = window.innerWidth * 0.8;
                this.$refs.canvasWrapper.style.width = `${this.width + 30}px`
                this.$refs.canvasWrapper.style.height = `${this.height}px`

                this.canvasContext = this.$refs.canvas.getContext('2d')
                this.canvasContext.lineJoin = 'round'
                this.canvasContext.lineCap = 'round'
                this.canvasContext.lineWidth = this.brushSize
                this.canvasContext.strokeStyle = this.tools[this.selectedToolIdx].color
                var background = new Image()
                background.crossOrigin = 'Anonymous'

                background.src = this.answerUrl || this.bgUrl

                background.onload = () => {
                    drawImageProp(this.canvasContext, background, 0, 0, this.width, this.height)
                }
                this.cursorContext = this.$refs.cursor.getContext('2d')
            },
            bindEvents: function () {
                this.$refs.canvas.addEventListener('mousedown', (event) => {
                    this.isDrawing = true;
                    [this.lastX, this.lastY] = [event.offsetX, event.offsetY]
                })

                this.$refs.canvas.addEventListener('touchstart', (e) => {
                    this.isDrawing = true
                    const rect = e.target.getBoundingClientRect()

                    const x = e.targetTouches[0].clientX - rect.left
                    const y = e.targetTouches[0].clientY - rect.top;
                    [this.lastX, this.lastY] = [x, y]
                })

                this.$refs.canvas.addEventListener('touchmove', this.drawTouch)
                this.$refs.canvas.addEventListener('touchend', () => this.isDrawing = false)

                this.$refs.canvas.addEventListener('mousemove', this.draw)
                this.$refs.canvas.addEventListener('mouseup', () => this.isDrawing = false)
                this.$refs.canvas.addEventListener('mouseout', () => this.isDrawing = false)
            },
            changeTool(tool) {
                this.selectedToolIdx = tool
            },
            draw(event) {
                this.drawCursor(event)
                if (!this.isDrawing) return

                if (this.tools[this.selectedToolIdx].name === 'Eraser') {
                    this.canvasContext.globalCompositeOperation = 'destination-out'
                } else {
                    this.canvasContext.globalCompositeOperation = 'source-over'
                    this.canvasContext.strokeStyle = this.tools[this.selectedToolIdx].color
                }

                this.canvasContext.beginPath()
                this.canvasContext.moveTo(this.lastX, this.lastY)
                this.canvasContext.lineTo(event.offsetX, event.offsetY)
                this.canvasContext.stroke();

                [this.lastX, this.lastY] = [event.offsetX, event.offsetY]
            },
            drawTouch(e) {
                if (!this.isDrawing) return

                if (this.tools[this.selectedToolIdx].name === 'Eraser') {
                    this.canvasContext.globalCompositeOperation = 'destination-out'
                } else {
                    this.canvasContext.globalCompositeOperation = 'source-over'
                    this.canvasContext.strokeStyle = this.tools[this.selectedToolIdx].color
                }
                const rect = e.target.getBoundingClientRect()

                const x = e.targetTouches[0].clientX - rect.left
                const y = e.targetTouches[0].clientY - rect.top

                this.canvasContext.beginPath()
                this.canvasContext.moveTo(this.lastX, this.lastY)
                this.canvasContext.lineTo(x, y)
                this.canvasContext.stroke();

                [this.lastX, this.lastY] = [x, y]
            },
            drawCursor(event) {
                this.cursorContext.beginPath()
                this.cursorContext.ellipse(
                    event.offsetX, event.offsetY,
                    this.brushSize, this.brushSize,
                    Math.PI / 4, 0, 2 * Math.PI
                )
                this.cursorContext.stroke()
                setTimeout(() => {
                    this.cursorContext.clearRect(0, 0, this.width, this.height)
                }, 100)
            },
            showColorPalette() {
                const colorPalette = document.createElement('input')
                colorPalette.addEventListener('change', (event) => {
                    this.tools[0].color = event.target.value
                })
                colorPalette.type = 'color'
                colorPalette.value = this.tools[0].color
                colorPalette.click()
            },
            upload() {
                const formData = new FormData()
                this.$refs.canvas.toBlob(el => {
                    formData.append('files', el, 'draw.jpeg')
                    this.isLoading = true
                    axios.post(imageServerUrl, formData).then(res => {
                        this.isLoading = false
                        this.$emit('file-uploaded', res.data)
                    }).catch(err => {
                        this.isLoading = false
                    })
                }, 'image/jpeg')
            }
        }
    }

    function drawImageProp(ctx, img, x, y, w, h, offsetX, offsetY) {
        if (arguments.length === 2) {
            x = y = 0
            w = ctx.canvas.width
            h = ctx.canvas.height
        }

        // default offset is center
        offsetX = typeof offsetX === 'number' ? offsetX : 0.5
        offsetY = typeof offsetY === 'number' ? offsetY : 0.5

        // keep bounds [0.0, 1.0]
        if (offsetX < 0) offsetX = 0
        if (offsetY < 0) offsetY = 0
        if (offsetX > 1) offsetX = 1
        if (offsetY > 1) offsetY = 1

        var iw = img.width,
            ih = img.height,
            r = Math.min(w / iw, h / ih),
            nw = iw * r, // new prop. width
            nh = ih * r, // new prop. height
            cx, cy, cw, ch, ar = 1

        // decide which gap to fill
        if (nw < w) ar = w / nw
        if (Math.abs(ar - 1) < 1e-14 && nh < h) ar = h / nh // updated
        nw *= ar
        nh *= ar

        // calc source rectangle
        cw = iw / (nw / w)
        ch = ih / (nh / h)

        cx = (iw - cw) * offsetX
        cy = (ih - ch) * offsetY

        // make sure source rectangle is valid
        if (cx < 0) cx = 0
        if (cy < 0) cy = 0
        if (cw > iw) cw = iw
        if (ch > ih) ch = ih

        // fill image in dest. rectangle
        ctx.drawImage(img, cx, cy, cw, ch, x, y, w, h)
    }
</script>
<style scoped>
    .canvas-wrapper {
        align-content: center;
        display: flex;
        position: relative;
    }

    #canvas {
        background-color: #f9f9f9;
        z-index: 0;
        touch-action: none;
    }

    #cursor {
        z-index: 1;
        pointer-events: none;
    }

    .active {
        background-color: #dea878 !important;
    }

    .tools {
        margin: 0;
        padding: 0;
    }

    .tools li {
        padding: 4px;
        background-color: #c8c8c8;
        border-left: 1px solid #abaaaa;
    }

    .tools li:not(:last-child) {
        border-bottom: 1px solid #abaaaa;
    }

    .draw-area canvas {
        position: absolute;
        left: 0;
        top: 0;
        border: 2px solid #c8c8c8;
        border-radius: 10px 0 10px 10px;
    }
</style>
