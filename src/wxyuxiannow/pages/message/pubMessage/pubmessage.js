Page({
    data: {
        filePath: "",
        images: [],
        uploadImgs: [],
        count: 9,
        array: ['招聘求职', '本地服务', '房屋出租', '征婚交友'],
        index: -1,
        textArea:''
    },
    bindPickerChange: function(e) {
        console.log('picker发送选择改变，携带值为', e.detail.value)
        this.setData({
            index: e.detail.value
        })
    },
    bindAreaBlur:function(e) {
      console.log(e.detail.value)
      this.setData({
          textArea:e.detail.value
      })
    },
    chooseImage: function (e) {
        var selectPictureNum = e.target.dataset.num;
        this.setData({
            count: 9 - selectPictureNum
        })
        var that = this;
        wx.chooseImage({
            count: that.data.count, // 默认9
            sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function (res) {
                var tempFilePaths = res.tempFilePaths;
                that.setData({
                    filePath: res.tempFilePaths[0],
                    images: that.data.images.concat(tempFilePaths),
                    uploadImgs: res.tempFilePaths
                })
            },
        })

    },
    uploadImg: function () {
        var that = this;
        console.log(JSON.stringify(that.data.uploadImgs))
        for (var i = 0; i < that.data.uploadImgs.length; i++) {
            var filePath = that.data.uploadImgs[i];

            uploadImage(
                {
                    filePath: filePath,
                    dir: "images/",
                    success: function (res) {
                        console.log("上传成功")
                        console.log("上传成功" + JSON.stringify(res))
                    },
                    fail: function (res) {
                        console.log("上传失败")
                        console.log(res)
                    }
                })
        }

    },
    uploadFile: function (params) {
        if (!params.filePath) {
            wx.showModal({
                title: '图片错误',
                content: '请重试',
                showCancel: false,
            })
            return;
        }
        
        wx.uploadFile({
            url: 'url',
            filePath: params.filePath,
            name: 'file',
            formData: {
                //'name': "picture.png",
                // 'key': aliyunFileKey,
                // 'policy': policyBase64,
                // 'OSSAccessKeyId': accessid,
                // 'signature': signature,
                'success_action_status': '200',
            },
            success: function (res) {
                if (res.statusCode != 200) {
                    if (params.fail) {
                        params.fail(res)
                    }
                    return;
                }
                if (params.success) {
                    params.success(aliyunFileKey);
                }
            },
            fail: function (err) {
                err.wxaddinfo = aliyunServerURL;
                if (params.fail) {
                    params.fail(err)
                }
            },
        })
    }
})