// pages/mine/CallFriends/index.js
const auth = require('../../../utils/auth');
const Api = require('../../../utils/httpPost');
const constants = require('../../../configs/constants');
import imageUtil from '../../../utils/image';
Page({

  /**
   * 页面的初始数据
   */
  data: {
    canvasHeight: 0,
  },
  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    auth.checkHasLogined().then(isLogined => {
      if (isLogined) {
        this.fetchQrcode();
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
  fetchQrcode() {
    const _this = this
    wx.showLoading({
      title: '加载中',
      mask: true
    })
    Api.Post("/api/Unlimited", { page: "/pages/mine/index" }).then(res => {
      wx.hideLoading();
      if (res.code == 200) {
        _this.showCanvas(res.data)
      } else {
        wx.showModal({
          title: '提示',
          content: res.message
        })
      }
    });
  },
  showCanvas(qrcode) {
    const _this = this
    let ctx
    wx.getImageInfo({
      src: qrcode,
      success: (res) => {
        const imageSize = imageUtil(res.width, res.height)
        const qrcodeWidth = imageSize.windowWidth / 2
        _this.setData({
          canvasHeight: qrcodeWidth
        })
        ctx = wx.createCanvasContext('firstCanvas')
        ctx.setFillStyle('#fff')
        ctx.fillRect(0, 0, imageSize.windowWidth, imageSize.imageHeight + qrcodeWidth)
        ctx.drawImage(res.path, (imageSize.windowWidth - qrcodeWidth) / 2, 0, qrcodeWidth, qrcodeWidth)
        setTimeout(function () {
          wx.hideLoading()
          ctx.draw()
        }, 1000)
      },
      fail: (res) => {
        console.log(res)
      }
    })
  },


  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {
    return {
      title: constants.shareProfile,
      path: '/pages/message/index?inviter_id=' + wx.getStorageSync('uid'),
      success: function (res) {
        // 转发成功
      },
      fail: function (res) {
        // 转发失败
      }
    }
  },
  saveToMobile() { //下载二维码到手机
    wx.canvasToTempFilePath({
      canvasId: 'firstCanvas',
      success: function (res) {
        let tempFilePath = res.tempFilePath
        wx.saveImageToPhotosAlbum({
          filePath: tempFilePath,
          success: (res) => {
            wx.showModal({
              content: '二维码已保存到手机相册',
              showCancel: false,
              confirmText: '知道了',
              confirmColor: '#333'
            })
          },
          fail: (res) => {
            wx.showToast({
              title: res.errMsg,
              icon: 'none',
              duration: 2000
            })
          }
        })
      }
    })
  }

})