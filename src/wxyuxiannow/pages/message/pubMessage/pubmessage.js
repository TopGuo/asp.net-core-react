const Api = require('../../../utils/httpPost');
const auth = require('../../../utils/auth');
const constant = require('../../../configs/constants');
Page({
    data: {
        baseImgs: [],
        images: [],
        count: 9,
        array: [],
        index: -1,
        types: -1,//类别
        textArea: '暂无'//内容
    },
    onLoad: function () {
        //验证是否登录
        auth.checkHasLogined().then(isLogined => {
            if (isLogined) {
                Api.Post('/api/MessageType', {}).then(res => {
                    let tempArry = [];
                    if (res.code == 200) {
                        res.data.map((v) => {
                            let tempObj = {};
                            tempObj.name = v.title;
                            tempObj.types = v.types
                            tempArry.push(tempObj);
                        });
                        this.setData({
                            array: tempArry
                        })
                    }
                })
            } else {
                wx.showModal({
                    title: '提示',
                    content: '本次操作需要您的登录授权',
                    cancelText: '暂不登录',
                    confirmText: '前往登录',
                    success(res) {
                        if (res.confirm) {
                            wx.switchTab({
                                url: "/pages/mine/index"
                            })
                        } else {
                            wx.navigateBack()
                        }
                    }
                })
            }
        })
    },
    bindPickerChange: function (e) {
        console.log('data', JSON.stringify(this.data.array))
        let type = this.data.array[e.detail.value].types;
        console.log('type', type)
        this.setData({
            index: e.detail.value,
            types: type
        })
    },
    bindAreaBlur: function (e) {
        this.setData({
            textArea: e.detail.value
        })
    },
    chooseImage: function (e) {
        var selectPictureNum = e.target.dataset.num;
        if (selectPictureNum >= 6) {
            wx.showModal({
                title: '提示',
                content: '最多上传6张图片',
                cancelText: '取消'
            })
            return;
        }
        var that = this;
        wx.chooseImage({
            sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function (res) {
                var tempFilePaths = res.tempFilePaths;
                that.setData({
                    images: that.data.images.concat(tempFilePaths)
                })
                that.uploadImg(tempFilePaths);
            }
        })

    },
    uploadImg: function (uploadImgs) {
        uploadImgs.map((v) => {
            this.uploadBase64File(v);
        });

    },
    uploadBase64File: function (v) {
        wx.getFileSystemManager().readFile({
            filePath: v, //选择图片返回的相对路径
            encoding: 'base64', //编码格式
            success: (res) => {
                let baseImg = 'data:image/png;base64,' + res.data
                this.data.baseImgs.push(baseImg);
            }
        })
    },
    pubMessage: function () {
        wx.showLoading()
        Api.Post('/api/PubMessage', { basePics: this.data.baseImgs, content: this.data.textArea, types: this.data.types }).then(res => {
            wx.hideLoading();
            if (res.code == 200) {
                wx.showModal({
                    title: '提示',
                    content: '发布信息成功',
                    confirmText: '确定',
                    success(res) {
                        if (res.confirm) {
                            wx.navigateBack()
                        }
                    }
                })
            } else {
                wx.showModal({
                    title: '提示',
                    content: '发布信息失败' + res.message
                })
            }
        })
    }
})