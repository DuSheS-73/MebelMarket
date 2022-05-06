<template>
	<div id="templateWrapper">
		<Header :key="$route.path"/>

		<router-view :key="$route.path"/>
		
		<Footer />
	</div>
</template>

<script>
	
	import Header from '@/components/HeaderComponent'
	import Footer from '@/components/FooterComponent'

	export default {
		name: 'MainLayout',
		components: { Header, Footer }
	}

</script>