<template>
	<div class="container mt-5">
		<div class="row">
			<div class="col-lg-12">
				<router-link :to="{name:'EditProduct'}">Добавить объявление</router-link>
			</div>
		</div>
	</div>
</template>

<script>

	import { useMeta } from 'vue-meta'
  import axios from 'axios'

	export default {

		setup () {
      useMeta({
        title: 'Личный кабинет'
      })
    }

	}
</script>