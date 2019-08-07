<template>
    <v-container fluid>
        <v-flex xs12 class="text-xs-center text-sm-center text-md-center text-lg-center">
            <img :src="imageUrl || initialImg" height="150" v-if="imageUrl" alt="no picture found"/>
            <v-text-field label="Kies een foto" novalidate @click='pickFile' v-model='imageName'
                          prepend-icon='attach_file'></v-text-field>
            <form enctype="multipart/form-data" novalidate>
                <input type="file"                        style="display: none"
                       ref="image"
                       accept="image/*" @change="onFilePicked" >
                <v-text-field
                label="Beschrijving" v-model="answer.description"
                ></v-text-field>
            </form>
        </v-flex>
        <v-btn color="success" v-on:click="save">
            <div v-if="!isLoading">Verstuur</div>
            <v-progress-circular v-else :indeterminate="true"></v-progress-circular>
        </v-btn>
    </v-container>
</template>

<script lang="js">
import * as axios from 'axios'
import {imageServerUrl} from '../services/ApiServices';
export default {
  name: 'FPicturePicker',
  props: { initialImg: String, answer: {} },
  data () {
    return {
      title: 'Image Upload',
      imageName: '',
      imageUrl: this.initialImg,
      imageFile: '',
      isLoading: false,

    }
  },
  mounted () {
    this.imageName = this.initialImg ? 'geuploade versie veranderen?' : '';
  },
  methods: {
    pickFile () {
      this.$refs.image.click()
    },

    onFilePicked (e) {
      const files = e.target.files
      if (files[0] !== undefined) {
        this.imageName = files[0].name
        if (this.imageName.lastIndexOf('.') <= 0) {
          return
        }
        const fr = new FileReader()
        fr.readAsDataURL(files[0])
        fr.addEventListener('load', () => {
          this.imageUrl = fr.result
          this.imageFile = files[0]
        })
      } else {
        this.imageName = ''
        this.imageFile = ''
        this.imageUrl = ''
      }
    },
    save () {
      const formData = new FormData()
      formData.append('files', this.imageFile, this.imageName)
      this.isLoading = true
      axios.post(imageServerUrl, formData).then(res => {
        this.isLoading = false
        this.$emit('file-uploaded', res.data)
      }).catch(err => {
        console.log(err)
        this.isLoading = false
      })
    }
  }
}
</script>

<style scoped>

</style>
