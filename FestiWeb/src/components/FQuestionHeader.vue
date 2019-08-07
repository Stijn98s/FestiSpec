<template>
    <div >
        <div class="left">{{question.order}} {{ question.description }}</div>

        <div class="right" v-if="!answer.id">
            <v-icon>assignment_late</v-icon>
        </div>
        <div class="right" v-if="answer.id">
            <v-icon color="success">check_circle_outline</v-icon>
        </div>
    </div>
</template>
<script lang="ts">
export default {
  name: 'f-question-header',
  props: ['question', 'answer']
}
</script>
