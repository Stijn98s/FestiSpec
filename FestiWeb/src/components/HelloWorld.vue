<!--
<template>
 <v-container fluid fill-height>
  <v-layout justify-center align-center>
  <v-expansion-panel  popout class=" mx-5">
      <f-open-question class="elevation-3" v-for="item in questions" :key="item.id" :entity="item" />
  </v-expansion-panel>
  </v-layout>
 </v-container>
</template>

<script lang="ts">

import { openQuestionClient } from '../services/ApiServices'
import FOpenQuestion from './FOpenQuestion.vue'
import { Component, Prop, Vue } from 'vue-property-decorator'
import { IOpenQuestion } from '../services/ApiService'
import { StateTrackable } from '../domain/StateTrackable'

@Component({
  components: {
  'FOpenQuestion': FOpenQuestion
  }
  })
export default class HelloWorld extends Vue {
  data () {
    return {}
  };

  get questions (): StateTrackable<IOpenQuestion>[] | null {
    return this.$store.getters.getQuestions
  }

  async mounted () {
    if (navigator.onLine) {
      openQuestionClient.getOpenQuestionAll().then(res => {
        this.$store.commit('commitResponse', res)
      }).catch()
    }
  }
}
</script>
-->
