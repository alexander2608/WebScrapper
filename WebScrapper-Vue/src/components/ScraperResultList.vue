<template>
  <form>
      <h1> Result </h1>
      <h3 class="result" v-if="positionArray.length>0">{{positionArray}}</h3>
      <li v-for="webScraperResult in GetWebScraperResultResponse.webScraperResultList" :key="webScraperResult.position" class="result">
          <span v-bind:class="{'match' : webScraperResult.isMatch == true}">{{webScraperResult.position}} - {{webScraperResult.address}}</span>
      </li>
      <h3 class="result" v-if="GetWebScraperResultResponse.isSuccess === false">{{GetWebScraperResultResponse.message}}</h3>
  </form>
</template>

<script>
    import ClientInput from './ClientInput.vue';
    export default{
        data(){
            return{
                positionArray:[]
            }
        },
        computed:{
            GetWebScraperResultResponse(){
                var response = this.$store.getters.GetWebScraperResultResponse;
                this.updatePositionString(response);
                return response;
            }
        },
        methods:{
            updatePositionString(response){
                if (response.webScraperResultList != null){
                    this.positionArray = []
                    response.webScraperResultList.forEach(webScraperResult => {
                        if(webScraperResult.isMatch == true){
                            this.positionArray.push(webScraperResult.position);
                        }
                    });
                }
            }
        }
    }
</script>

<style scoped>
.item {
  margin-top: 2rem;
  display: flex;
}
.form {
    min-width: 400px;
    margin: 30px auto;
    background:gray;
    text-align: left;
    padding: 40px;
    border-radius: 50px;
}
.match{
    color: yellow;
}
.result{
    color: white
}
</style>
